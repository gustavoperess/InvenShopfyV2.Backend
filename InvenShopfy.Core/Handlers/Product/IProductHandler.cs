using InvenShopfy.Core.Requests.Products.Product;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.Core.Handlers.Product;

public interface IProductHandler
{
    Task<Response<Models.Product.Product?>> CreateProductAsync(CreateProductRequest request);
    Task<Response<Models.Product.Product?>> UpdateProductAsync(UpdateProductRequest request);
    Task<Response<Models.Product.Product?>> DeleteProductAsync(DeleteProductRequest request);
    Task<Response<Models.Product.Product?>> GetProductByIdAsync(GetProductByIdRequest request);
    Task<PagedResponse<List<Models.Product.ProductByName>?>> GetProductByPartialNameAsync(GetProductByNameRequest request);
    Task<PagedResponse<List<Models.Product.ProductList>?>> GetProductByPeriodAsync(GetAllProductsRequest request);
    
    
}