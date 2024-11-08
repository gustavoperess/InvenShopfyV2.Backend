using InvenShopfy.API.Data;
using InvenShopfy.Core.Handlers.Tradings.Purchase;
using InvenShopfy.Core.Models.Tradings.Purchase;
using InvenShopfy.Core.Models.Tradings.Purchase.Dto;
using InvenShopfy.Core.Models.Warehouse;
using InvenShopfy.Core.Requests.Tradings.Purchase.AddPurchase;
using InvenShopfy.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace InvenShopfy.API.Handlers.Tradings.Purchase;

public class PurchaseHandler(AppDbContext context) : IPurchaseHandler
{
    public async Task<Response<AddPurchase?>> CreatePurchaseAsync(CreatePurchaseRequest request)
    {
        try
        {
            var purchase = new AddPurchase
            {
                UserId = request.UserId,
                WarehouseId = request.WarehouseId,
                SupplierId = request.SupplierId,
                PurchaseStatus = request.PurchaseStatus,
                ShippingCost = request.ShippingCost,
                PurchaseNote = request.PurchaseNote,
                PurchaseDate = request.PurchaseDate,
                TotalAmountBought = request.TotalAmountBought,
            };

            // Validate and add products to purchase
            var productIds = request.ProductIdPlusQuantity.Keys;
            var availablePurchaseProducts = await context.Products
                .Where(sp => productIds.Contains(sp.Id)).ToListAsync();

            var purchaseResponse =
                purchase.AddToPurchaseProduct(request.ProductIdPlusQuantity, availablePurchaseProducts);
            if (!purchaseResponse.IsSuccess)
            {
                return purchaseResponse;
            }

            // Retrieve warehouse products for this warehouse
            var warehouseProducts = await context.WarehousesProducts
                .Where(x => x.WarehouseId == request.WarehouseId).ToListAsync();

            // Update warehouse inventory
            foreach (var (productId, quantity) in request.ProductIdPlusQuantity)
            {
                var updatedWarehouseProduct = new WarehouseProduct();
                updatedWarehouseProduct = updatedWarehouseProduct.AddProductIdAndAmountToWarehouse(
                    new Dictionary<long, int> { { productId, quantity } },
                    warehouseProducts,
                    request.WarehouseId);

                if (updatedWarehouseProduct != null)
                {
                    context.WarehousesProducts.Add(updatedWarehouseProduct);
                }
            }
            
            await using var transaction = await context.Database.BeginTransactionAsync();
            await context.Purchases.AddAsync(purchase);
            await context.SaveChangesAsync();
            await transaction.CommitAsync();

            return new Response<AddPurchase?>(purchase, 201, "Purchase created successfully");
        }
        catch
        {
            return new Response<AddPurchase?>(null, 500, "It was not possible to create a new purchase");
        }
    }


    public async Task<Response<AddPurchase?>> UpdatePurchaseAsync(UpdatePurchaseRequest request)
    {
        try
        {
            var purchase =
                await context.Purchases.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (purchase is null)
            {
                return new Response<AddPurchase?>(null, 404, "Purchase not found");
            }

            purchase.WarehouseId = request.WarehouseId;
            purchase.SupplierId = request.SupplierId;
            purchase.PurchaseStatus = request.PurchaseStatus;
            purchase.ShippingCost = request.ShippingCost;
            purchase.PurchaseNote = request.PurchaseNote;
            context.Purchases.Update(purchase);
            await context.SaveChangesAsync();
            return new Response<AddPurchase?>(purchase, message: "Purchase updated successfully");
        }
        catch
        {
            return new Response<AddPurchase?>(null, 500, "It was not possible to update this Purchase");
        }
    }

    public async Task<Response<AddPurchase?>> DeletePurchaseAsync(DeletePurchaseRequest request)
    {
        try
        {
            var purchase =
                await context.Purchases.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (purchase is null)
            {
                return new Response<AddPurchase?>(null, 404, "purchase not found");
            }

            context.Purchases.Remove(purchase);
            await context.SaveChangesAsync();
            return new Response<AddPurchase?>(purchase, message: "purchase removed successfully");
        }
        catch
        {
            return new Response<AddPurchase?>(null, 500, "It was not possible to delete this purchase");
        }
    }

    public async Task<Response<List<PurchasePerProduct>?>> GetPurchaseByIdAsync(GetPurchaseByIdRequest request)
    {
        try
        {
            var query = context
                .PurchaseProducts
                .AsNoTracking()
                .Include(x => x.AddPurchase)
                .Include(x => x.Product)
                .Where(x => x.AddPurchase.UserId == request.UserId && x.AddPurchaseId == request.PurchaseId)
                .GroupBy(x => new
                {
                    x.ProductId, x.Product.Title, x.PurchaseReferenceNumber, x.TotalPricePaidPerProduct,
                    x.TotalQuantityBoughtPerProduct,
                    x.Product.Unit.ShortName, x.AddPurchase.TotalAmountBought, x.Product.Price,
                    x.AddPurchase.ShippingCost,
                    x.AddPurchase.PurchaseNote, x.AddPurchase.Supplier.Name, x.AddPurchase.Supplier.Email
                }).Select(g => new
                {
                    Id = g.Key.ProductId,
                    ProductPrice = g.Key.Price,
                    ProductName = g.Key.Title,
                    UnitShortName = g.Key.ShortName,
                    SupplierName = g.Key.Name,
                    SupplierEmail = g.Key.Email,
                    g.Key.PurchaseNote,
                    g.Key.ShippingCost,
                    g.Key.TotalAmountBought,
                    g.Key.TotalPricePaidPerProduct,
                    g.Key.PurchaseReferenceNumber,
                    g.Key.TotalQuantityBoughtPerProduct,
                });

            var purchase = await query.ToListAsync();
            var result = purchase.Select(s => new PurchasePerProduct
            {
                TotalAmount = s.TotalAmountBought,
                ProductPrice = s.ProductPrice,
                ProductId = s.Id,
                ProductName = s.ProductName,
                UnitShortName = s.UnitShortName,
                ReferenceNumber = s.PurchaseReferenceNumber,
                TotalPricePaidPerProduct = s.TotalPricePaidPerProduct,
                TotalQuantityBoughtPerProduct = s.TotalQuantityBoughtPerProduct,
                ShippingCost = s.ShippingCost,
                PurchaseNote = s.PurchaseNote,
                SupplierName = s.SupplierName,
                SupplierEmail = s.SupplierEmail
            }).ToList();
            if (result.Count == 0)
            {
                return new Response<List<PurchasePerProduct>?>(result, 400, "No item found with this Id");
            }

            return new Response<List<PurchasePerProduct>?>(result, 200, "Items retrived Successfully");
        }
        catch
        {
            return new Response<List<PurchasePerProduct>?>(null, 500, "It was not possible to consult all purchases");
        }
    }

    public async Task<PagedResponse<List<PurchaseList>?>> GetPurchaseByPeriodAsync(GetAllPurchasesRequest request)
    {
        try
        {
            var query = context
                .Purchases
                .AsNoTracking()
                .Include(x => x.Warehouse)
                .Include(x => x.Supplier)
                .Where(x => x.UserId == request.UserId)
                .Select(g => new
                {
                    g.Id,
                    g.PurchaseDate,
                    SupplierName = g.Supplier.Name,
                    g.Warehouse.WarehouseName,
                    g.PurchaseStatus,
                    g.ShippingCost,
                    g.TotalAmountBought,
                    g.ReferenceNumber,
                    g.TotalNumberOfProductsBought
                }).OrderBy(x => x.PurchaseDate);

            var purchase = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            var result = purchase.Select(p => new PurchaseList
            {
                Id = p.Id,
                PurchaseDate = p.PurchaseDate,
                SupplierName = p.SupplierName,
                WarehouseName = p.WarehouseName,
                PurchaseStatus = p.PurchaseStatus,
                ShippingCost = p.ShippingCost,
                TotalAmountBought = p.TotalAmountBought,
                ReferenceNumber = p.ReferenceNumber,
                TotalNumberOfProductsBought = p.TotalNumberOfProductsBought
            }).ToList();

            var count = await query.CountAsync();

            return new PagedResponse<List<PurchaseList>?>(
                result,
                count,
                request.PageNumber,
                request.PageSize);
        }
        catch
        {
            return new PagedResponse<List<PurchaseList>?>(null, 500, "It was not possible to consult all purchases");
        }
    }
}