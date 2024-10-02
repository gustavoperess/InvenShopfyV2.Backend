using InvenShopfy.Core.Requests.Products.Product;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.Core.Handlers.Product;

public interface IProductHandler
{
    Task<Response<Models.Product.Product?>> CreateAsync(CreateProductRequest request);
    Task<Response<Models.Product.Product?>> UpdateAsync(UpdateProductRequest request);
    Task<Response<Models.Product.Product?>> DeleteAsync(DeleteProductRequest request);
    Task<Response<Models.Product.Product?>> GetByIdAsync(GetProductByIdRequest request);
    Task<PagedResponse<List<Models.Product.ProductByName>?>> GetByPartialNameAsync(GetProductByNameRequest request);
    Task<PagedResponse<List<Models.Product.ProductList>?>> GetByPeriodAsync(GetAllProductsRequest request);
    
    
}