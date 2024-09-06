using InvenShopfy.Core.Requests.Products.Brand;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.Core.Handlers.Product;

public interface IBrandHandler 
{
    Task<Response<Models.Product.Brand?>> CreateAsync(CreateBrandRequest request);
    Task<Response<Models.Product.Brand?>> UpdateAsync(UpdateBrandRequest request);
    Task<Response<Models.Product.Brand?>> DeleteAsync(DeleteBrandRequest request);
    Task<Response<Models.Product.Brand?>> GetByIdAsync(GetBrandByIdRequest request);
    Task<PagedResponse<List<Models.Product.Brand>?>> GetByPeriodAsync(GetAllBrandsRequest request);
}