// using InvenShopfy.Core.Handlers.Tradings.Purchase;
// using InvenShopfy.Core.Models.Product;
// using InvenShopfy.Core.Models.Tradings.Purchase;
// using InvenShopfy.Core.Requests.Products.Brand;
// using InvenShopfy.Core.Requests.Tradings.Purchase.Add;
// using InvenShopfy.Core.Responses;
//
// namespace InvenShopfy.API.Handlers.Tradings.Purchase;
//
// public class AddHandler : IAddHandler
// {
//     public async Task<Response<Add?>> CreateAsync(CreatePurchaseRequest request)
//     {
//         try
//         {
//             var purchase = new Purchase
//             {
//                 UserId = request.UserId,
//                 Title = request.Title,
//                 BrandImage = request.BrandImage
//             };
//             await context.Brands.AddAsync(purchase);
//             await context.SaveChangesAsync();
//
//             return new Response<Add?>(purchase, 201, "purchase created successfully");
//
//         }
//         catch
//         {
//             return new Response<Add?>(null, 500, "It was not possible to create a new purchase");
//         }
//     }
//
//     public async Task<Response<Brand?>> UpdateAsync(UpdateBrandRequest request)
//     {
//         try
//         {
//             var brand = await context.Brands.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
//
//             if (brand is null)
//             {
//                 return new Response<Brand?>(null, 404, "Brand not found");
//             }
//
//             brand.Title = request.Title;
//             brand.BrandImage = request.BrandImage;
//             context.Brands.Update(brand);
//             await context.SaveChangesAsync();
//             return new Response<Brand?>(brand, message: "brand updated successfully");
//
//         }
//         catch
//         {
//             return new Response<Brand?>(null, 500, "It was not possible to update this Brand");
//         }
//     }
//
//     public async Task<Response<Brand?>> DeleteAsync(DeleteBrandRequest request)
//     {
//         try
//         {
//             var brand = await context.Brands.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
//
//             if (brand is null)
//             {
//                 return new Response<Brand?>(null, 404, "Brand not found");
//             }
//
//             context.Brands.Remove(brand);
//             await context.SaveChangesAsync();
//             return new Response<Brand?>(brand, message: "brand removed successfully");
//
//         }
//         catch
//         {
//             return new Response<Brand?>(null, 500, "It was not possible to delete this Brand");
//         }
//     }
//
//     public async Task<Response<Brand?>> GetByIdAsync(GetBrandByIdRequest request)
//     {
//         try
//         {
//             var brand = await context.Brands.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
//
//             if (brand is null)
//             {
//                 return new Response<Brand?>(null, 404, "Brand not found");
//             }
//
//             return new Response<Brand?>(brand);
//
//         }
//         catch
//         {
//             return new Response<Brand?>(null, 500, "It was not possible to find this Brand");
//         }
//     }
//
//     public async Task<PagedResponse<List<Brand>?>> GetByPeriodAsync(GetAllBrandsRequest request)
//     {
//         try
//         {
//             var query = context
//                 .Brands
//                 .AsNoTracking()
//                 .Where(x => x.UserId == request.UserId)
//                 .OrderBy(x => x.Title);
//
//             var brands = await query
//                 .Skip((request.PageNumber - 1) * request.PageSize)
//                 .Take(request.PageSize)
//                 .ToListAsync();
//
//             var count = await query.CountAsync();
//
//             return new PagedResponse<List<Brand>?>(
//                 brands,
//                 count,
//                 request.PageNumber,
//                 request.PageSize);
//         }
//         catch
//         {
//             return new PagedResponse<List<Brand>?>(null, 500, "It was not possible to consult all brands");
//         }
//     }
// }