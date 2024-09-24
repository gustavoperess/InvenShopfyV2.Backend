using InvenShopfy.Core.Requests.Products.Product;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.Core.Handlers.Product;

public interface IProductHandler
{
    Task<Response<Models.Product.Product?>> CreateAsync(CreateProductRequest request);
    Task<Response<Models.Product.Product?>> UpdateAsync(UpdateProductRequest request);
    Task<Response<Models.Product.Product?>> DeleteAsync(DeleteProductRequest request);
    Task<Response<Models.Product.Product?>> GetByIdAsync(GetProductByIdRequest request);
    Task<Response<Models.Product.Product?>> GeyByNameAsync(GetProductByNameRequest request);
    Task<PagedResponse<List<Models.Product.Product>?>> GetByPeriodAsync(GetAllProductsRequest request);
    
    
}