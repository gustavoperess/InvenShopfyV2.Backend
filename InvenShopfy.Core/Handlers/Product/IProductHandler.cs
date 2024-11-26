using InvenShopfy.Core.Models.Product.Dto;
using InvenShopfy.Core.Requests.Products.Product;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.Core.Handlers.Product;

public interface IProductHandler
{
    Task<Response<Models.Product.Product?>> CreateProductAsync(CreateProductRequest request);
    Task<Response<ProductByNameForUpdatePage?>> UpdateProductAsync(UpdateProductRequest request);
    Task<Response<Models.Product.Product?>> DeleteProductAsync(DeleteProductRequest request);
    Task<Response<Models.Product.Product?>> GetProductByIdAsync(GetProductByIdRequest request);
    Task<PagedResponse<List<ProductByName>?>> GetProductByPartialNameAsync(GetProductByNameRequest request);
    Task<PagedResponse<List<ProductByNameForUpdatePage>?>> GetProductByPartialNameForUpdatePageAsync(GetProductByNameRequest request);

    Task<PagedResponse<List<ProductList>?>> GetProductByPeriodAsync(GetAllProductsRequest request);
    
    
}