using InvenShopfy.Core.Requests.Products.Category;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.Core.Handlers.Product;

public interface ICategoryHandler
{
    Task<Response<Models.Product.Category?>> CreateProductCategoryAsync(CreateCategoryRequest request);
    Task<Response<Models.Product.Category?>> UpdateProductCategoryAsync(UpdateCategoryRequest request);
    Task<Response<Models.Product.Category?>> DeleteProductCategoryAsync(DeleteCategoryRequest request);
    Task<Response<Models.Product.Category?>> GetProductCategoryByIdAsync(GetCategoryByIdRequest request);
    Task<PagedResponse<List<Models.Product.Category>?>> GetProductCateogyByPeriodAsync(GetAllCategoriesRequest request);
}