using InvenShopfy.Core.Requests.Products.Brand;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.Core.Handlers.Product;

public interface IBrandHandler 
{
    Task<Response<Models.Product.Brand?>> CreateProductBrandAsync(CreateBrandRequest request);
    Task<Response<Models.Product.Brand?>> UpdateProductBrandAsync(UpdateBrandRequest request);
    Task<Response<Models.Product.Brand?>> DeleteProductBrandAsync(DeleteBrandRequest request);
    Task<Response<Models.Product.Brand?>> GetProductBrandByIdAsync(GetBrandByIdRequest request);
    Task<PagedResponse<List<Models.Product.Brand>?>> GetProductBrandByPeriodAsync(GetAllBrandsRequest request);
}