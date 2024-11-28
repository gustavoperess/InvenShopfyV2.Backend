using System.Globalization;
using InvenShopfy.API.Data;
using InvenShopfy.Core.Handlers.Product;
using InvenShopfy.Core.Models.Product;
using InvenShopfy.Core.Requests.Products.Category;
using InvenShopfy.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace InvenShopfy.API.Handlers.Products;

public class CategoryHandler(AppDbContext context) : ICategoryHandler

{
     public async Task<Response<Category?>> CreateProductCategoryAsync(CreateCategoryRequest request)
    {   
        try
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            var existingCategory = await context.Categories.FirstOrDefaultAsync(c =>
                c.MainCategory == request.MainCategory);
            if (existingCategory != null)
            {
                foreach (var subcategory in request.SubCategory)
                {
                    if (!existingCategory.SubCategory.Contains(subcategory))
                    {
                        existingCategory.SubCategory.Add(subcategory);
                    }
                }

                context.Categories.Update(existingCategory);
                await context.SaveChangesAsync();
                return new Response<Category?>(existingCategory, 200, "Subcategory appended to existing category");
            }
            
            var category = new Category
            {
                SubCategory = request.SubCategory,
                MainCategory = textInfo.ToTitleCase(request.MainCategory)
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

    public async Task<Response<Category?>> UpdateProductCategoryAsync(UpdateCategoryRequest request)
    {
        try
        {
            var category  = await context.Categories.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (category is null)
            {
                return new Response<Category?>(null, 404, "Category not found");
            }
            
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

    public async Task<Response<Category?>> DeleteProductCategoryAsync(DeleteCategoryRequest request)
    {
        try
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == request.Id);
            
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
    
    public async Task<Response<Category?>> GetProductCategoryByIdAsync(GetCategoryByIdRequest request)
    {
        try
        {
            var category = await context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id);
            
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
    public async Task<PagedResponse<List<Category>?>> GetProductCateogyByPeriodAsync(GetAllCategoriesRequest request)
    {
        try
        {
            var query = context
                .Categories
                .AsNoTracking()
                .OrderBy(x => x.MainCategory);
            
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