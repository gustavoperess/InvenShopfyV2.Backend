using System.Globalization;
using InvenShopfy.API.Data;
using InvenShopfy.Core.Handlers.People;
using InvenShopfy.Core.Models.People;
using InvenShopfy.Core.Models.People.Dto;
using InvenShopfy.Core.Requests.People.Supplier;
using InvenShopfy.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace InvenShopfy.API.Handlers.People;

public class SupplierHandler (AppDbContext context) : ISupplierHandler
{
      public async Task<Response<Supplier?>> CreateSupplierAsync(CreateSupplierRequest request)
    {
        try
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            var existingSupplier = await context.Suppliers
                .FirstOrDefaultAsync(x => x.Name.ToLower() == request.Name.ToLower() || 
                                          x.Email.ToLower() == request.Email.ToLower());
           
            if (existingSupplier != null)
            {
                if (existingSupplier.Name.ToLower() == request.Name.ToLower())
                {
                    return new Response<Supplier?>(null, 409, $"The supplier name '{request.Name}' already exists.");
                }
                if (existingSupplier.Email.ToLower() == request.Email.ToLower())
                {
                    return new Response<Supplier?>(null, 409, $"The supplier email '{request.Email}' already exists.");
                }
            }
            
            var supplier = new Supplier
            {
                UserId = request.UserId,
                Name = textInfo.ToTitleCase(request.Name),
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                SupplierCode = request.SupplierCode,
                Country = textInfo.ToTitleCase(request.Country),
                City = request.City,
                Address = textInfo.ToTitleCase(request.Address),
                ZipCode = request.ZipCode,
                Company = request.Company,
            };
            await context.Suppliers.AddAsync(supplier);
            await context.SaveChangesAsync();

            return new Response<Supplier?>(supplier, 201, "Supplier created successfully");

        }
        catch
        {
            return new Response<Supplier?>(null, 500, "It was not possible to create a new supplier");
        }
    }

    public async Task<Response<Supplier?>> UpdateSupplierAsync(UpdateSupplierRequest request)
    {
        try
        {
            var supplier = await context.Suppliers.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (supplier is null)
            {
                return new Response<Supplier?>(null, 404, "supplier not found");
            }
            
            supplier.UserId = request.UserId;
            supplier.Name = request.Name;
            supplier.PhoneNumber = request.PhoneNumber;
            supplier.Email = request.Email;
            supplier.Country = request.Country;
            supplier.City = request.City;
            supplier.Address = request.Address;
            supplier.ZipCode = request.ZipCode;
            supplier.SupplierCode = request.SupplierCode;
            supplier.Company = request.Company;
            
            context.Suppliers.Update(supplier);
            await context.SaveChangesAsync();
            return new Response<Supplier?>(supplier, message: "Supplier updated successfully");

        }
        catch 
        {
            return new Response<Supplier?>(null, 500, "It was not possible to update this Supplier");
        }
    }

    public async Task<Response<Supplier?>> DeleteSupplierAsync(DeleteSupplierRequest request)
    {
        try
        {
            var supplier = await context.Suppliers.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
            
            if (supplier is null)
            {
                return new Response<Supplier?>(null, 404, "Supplier not found");
            }

            context.Suppliers.Remove(supplier);
            await context.SaveChangesAsync();
            return new Response<Supplier?>(supplier, message: "Supplier removed successfully");

        }
        catch 
        {
            return new Response<Supplier?>(null, 500, "It was not possible to delete this supplier");
        }
    }
    
    public async Task<Response<Supplier?>> GetSupplierByIdAsync(GetSupplierByIdRequest request)
    {
        try
        {
            var supplier = await context.Suppliers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
            
            if (supplier is null)
            {
                return new Response<Supplier?>(null, 404, "Supplier not found");
            }

            return new Response<Supplier?>(supplier);

        }
        catch 
        {
            return new Response<Supplier?>(null, 500, "It was not possible to find this supplier");
        }
    }
    public async Task<PagedResponse<List<Supplier>?>> GetSupplierByPeriodAsync(GetAllSuppliersRequest request)
    {
        try
        {
            var query = context
                .Suppliers
                .AsNoTracking()
                .Where(x => x.UserId == request.UserId)
                .OrderBy(x => x.Name);
            
            var supplier = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
            
            var count = await query.CountAsync();
            
            return new PagedResponse<List<Supplier>?>(
                supplier,
                count,
                request.PageNumber,
                request.PageSize);
        }
        catch 
        {
            return new PagedResponse<List<Supplier>?>(null, 500, "It was not possible to consult all suppliers");
        }
    }
    
    public async Task<PagedResponse<List<SupplierName>?>> GetSupplierNameAsync(GetAllSuppliersRequest request)
    {
        try
        {
            var query = context
                .Suppliers
                .AsNoTracking()
                .Where(x => x.UserId == request.UserId)
                .Select(x => new SupplierName
                { 
                    Id = x.Id,
                    Name = x.Name
                    
                })
                .OrderBy(x => x.Name);
            
            var supplier = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
            
            var count = await query.CountAsync();
            
            return new PagedResponse<List<SupplierName>?>(
                supplier,
                count,
                request.PageNumber,
                request.PageSize);
        }
        catch 
        {
            return new PagedResponse<List<SupplierName>?>(null, 500, "It was not possible to consult all supplier names");
        }
    }
    public async Task<Response<List<TopSupplier>?>> GetTopSuppliersAsync(GetAllSuppliersRequest request)
    {
        try
        {
            var query = context
                .Purchases
                .AsNoTracking()
                .Include(x => x.Supplier)
                .Where(x => x.UserId == request.UserId)
                .GroupBy(y => new
                {
                    y.Supplier.Name,
                    y.SupplierId,
                    y.Supplier.SupplierCode,
                    y.Supplier.Company,
                })
                .Select(x => new TopSupplier
                { 
                    Id = x.Key.SupplierId,
                    Name = x.Key.Name,
                    SupplierCode = x.Key.SupplierCode,
                    Company = x.Key.Company,
                    TotalPurchase =  x.Sum(s => s.TotalAmountBought),
                    
                }).OrderByDescending(x => x.TotalPurchase).Take(5);
            
            var topSuppliers = await query.ToListAsync(); 
            
            return new Response<List<TopSupplier>?>(topSuppliers, 201, "top 05 suppliers returned successfully");
        }
        catch 
        {
            return new PagedResponse<List<TopSupplier>?>(null, 500, "It was not possible to return the top 05 suppliers");
        }
    }
}