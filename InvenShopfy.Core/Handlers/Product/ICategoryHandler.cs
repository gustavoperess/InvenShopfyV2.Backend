using InvenShopfy.Core.Requests.Category;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.Core.Handlers.Product;

public interface ICategoryHandler
{
    Task<Response<Models.Product.Category?>> CreateAsync(CreateCategoryRequest request);
    Task<Response<Models.Product.Category?>> UpdateAsync(UpdateCategoryRequest request);
    Task<Response<Models.Product.Category?>> DeleteAsync(DeleteCategoryRequest request);
    Task<Response<Models.Product.Category?>> GetByIdAsync(GetCategoryByIdRequest request);
    Task<PagedResponse<List<Models.Product.Category>?>> GetByPeriodAsync(GetAllCategoriesRequest request);
}