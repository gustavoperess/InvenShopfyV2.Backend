using InvenShopfy.API.Data;
using InvenShopfy.Core.Handlers.Product;
using InvenShopfy.Core.Models.Product;
using InvenShopfy.Core.Requests.Category;
using InvenShopfy.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace InvenShopfy.API.Handlers;

public class CategoryHandler(AppDbContext context) : ICategoryHandler

{
     public async Task<Response<Category?>> CreateAsync(CreateCategoryRequest request)
    {
        try
        {
            var category = new Category
            {
                UserId = request.UserId,
                Title = request.Title,
                SubCategory = request.SubCategory,
                MainCategory = request.MainCategory
            };
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();

            return new Response<Category?>(category, 201, "category created successfully");

        }
        catch
        {
            return new Response<Category?>(null, 500, "It was not possible to create a new category");
        }
    }

    public async Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request)
    {
        try
        {
            var category  = await context.Categories.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (category is null)
            {
                return new Response<Category?>(null, 404, "Category not found");
            }
            
            category.Title = request.Title;
            category.MainCategory = request.MainCategory;
            category.SubCategory = request.SubCategory;
            context.Categories.Update(category);
            await context.SaveChangesAsync();
            return new Response<Category?>(category, message: "category updated successfully");

        }
        catch 
        {
            return new Response<Category?>(null, 500, "It was not possible to update this Category");
        }
    }

    public async Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request)
    {
        try
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
            
            if (category is null)
            {
                return new Response<Category?>(null, 404, "category not found");
            }

            context.Categories.Remove(category);
            await context.SaveChangesAsync();
            return new Response<Category?>(category, message: "category removed successfully");

        }
        catch 
        {
            return new Response<Category?>(null, 500, "It was not possible to delete this category");
        }
    }
    
    public async Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequest request)
    {
        try
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
            
            if (category is null)
            {
                return new Response<Category?>(null, 404, "category not found");
            }

            return new Response<Category?>(category);

        }
        catch 
        {
            return new Response<Category?>(null, 500, "It was not possible to find this category");
        }
    }
    public async Task<PagedResponse<List<Category>?>> GetByPeriodAsync(GetAllCategoriesRequest request)
    {
        try
        {
            var query = context
                .Categories
                .AsNoTracking()
                .Where(x => x.UserId == request.UserId)
                .OrderBy(x => x.Title);
            
            var categories = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
            
            var count = await query.CountAsync();
            
            return new PagedResponse<List<Category>?>(
                categories,
                count,
                request.PageNumber,
                request.PageSize);
        }
        catch 
        {
            return new PagedResponse<List<Category>?>(null, 500, "It was not possible to consult all Categories");
        }
    }
}