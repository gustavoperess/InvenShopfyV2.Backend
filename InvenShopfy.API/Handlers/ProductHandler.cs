// using InvenShopfy.Core.Handlers.Product;
// using InvenShopfy.Core.Models.Product;
// using InvenShopfy.Core.Requests.Product;
// using InvenShopfy.Core.Responses;
//
// namespace InvenShopfy.API.Handlers;
//
// public class ProductHandler(AppContext context) : IProductHandler
// {
//     public async Task<Response<Product?>> CreateAsync(CreateProductRequest request)
//     {
//         try
//         {
//             var product = new Product
//             {
//                 UserId = request.UserId,
//                 Title = request.Title,
//                 Price = request.Price,
//                 ProductCode = request.ProductCode,
//                 CreateAt = DateTime.Now,
//                 UnitId = request.UnitId,
//                 BrandId = request.BrandId,
//                 CategoryId = request.CategoryId,
//                 ProductImage = request.ProductImage
//
//             };
//             await context.Products.AddAsync(product);
//             await context.SaveChangesAsync();
//             return new Response<Product?>(product, 201, "Product created successfully");
//         }
//         catch
//         {
//             return new Response<Product?>(null, 500, "It was not possible to create a new Transaction");
//         }
//     }
// }