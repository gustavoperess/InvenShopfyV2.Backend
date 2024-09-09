using InvenShopfy.API.Common.Api;
using InvenShopfy.API.EndPoints.Expenses.Category;
using InvenShopfy.API.EndPoints.Expenses.Expense;
using InvenShopfy.API.EndPoints.People.Biller;
using InvenShopfy.API.EndPoints.People.Customer;
using InvenShopfy.API.EndPoints.People.Supplier;
using InvenShopfy.API.EndPoints.Products.Brands;
using InvenShopfy.API.EndPoints.Products.Categories;
using InvenShopfy.API.EndPoints.Products.Product;
using InvenShopfy.API.EndPoints.Products.Units;
using InvenShopfy.API.EndPoints.UserManagement.Role;
using InvenShopfy.API.EndPoints.UserManagement.User;
using InvenShopfy.API.EndPoints.Warehouses;


namespace InvenShopfy.API.EndPoints;

public static class Endpoint
{
    public static void MapEndpoints(this WebApplication app)
    {
        var endpoints = app.MapGroup("");

        // Health Check
        endpoints.MapGroup("/").WithTags("Health Check").MapGet("/", () => new { message = "Ok" });

        // Products Group
        var productsGroup = endpoints.MapGroup("v2/products")
            .WithTags("products");

        productsGroup.MapGroup("Product")
            .WithTags("Products - product")
            .MapEndpoint<CreateProductEndpoint>()
            .MapEndpoint<UpdateProductEndpoint>()
            .MapEndpoint<GetAllProductsEndpoints>()
            .MapEndpoint<DeleteProductEndpoint>()
            .MapEndpoint<GetProductByIdEndpoint>();

        productsGroup.MapGroup("Brands")
            .WithTags("Products - brands")
            .MapEndpoint<CreateBrandEndpoint>()
            .MapEndpoint<UpdateBrandEndpoint>()
            .MapEndpoint<GetAllBrandsEndpoint>()
            .MapEndpoint<DeleteBrandEndpoint>()
            .MapEndpoint<GetBrandByIdEndpoint>();

        productsGroup.MapGroup("Units")
            .WithTags("Products - units")
            .MapEndpoint<CreateUnitEndpoint>()
            .MapEndpoint<UpdateUnitEndpoint>()
            .MapEndpoint<GetAllUnitsEndpoint>()
            .MapEndpoint<DeleteUnitEndpoint>()
            .MapEndpoint<GetUnitByIdEndpoint>();

        productsGroup.MapGroup("ProductCategory")
            .WithTags("Products - categories")
            .MapEndpoint<CreateCategoryEndpoint>()
            .MapEndpoint<UpdateCategoryEndpoint>()
            .MapEndpoint<GetAllCategoriesEndpoint>()
            .MapEndpoint<DeleteCategoryEndpoint>()
            .MapEndpoint<GetCategoryByIdEndpoint>();
        
        // Expense Group
        var expenseGroup = endpoints.MapGroup("v2/expenses")
            .WithTags("Expenses");
        
        expenseGroup.MapGroup("Expenses")
            .WithTags("Expenses - expenses")
            .MapEndpoint<CreateExpenseEndpoint>()
            .MapEndpoint<UpdateExpenseEndpoint>()
            .MapEndpoint<GetAllExpensesEndpoint>()
            .MapEndpoint<DeleteExpenseEndpoint>()
            .MapEndpoint<GetExpenseByIdEndpoint>();

        expenseGroup.MapGroup("ExpenseCategory")
            .WithTags("Expenses - category")
            .MapEndpoint<CreateExpenseCategoryEndpoint>()
            .MapEndpoint<UpdateExpenseCategoryEndpoint>()
            .MapEndpoint<GetAllExpenseCategoriesEndpoint>()
            .MapEndpoint<DeleteExpenseCategoryEndpoint>()
            .MapEndpoint<GetExpenseCategoryByIdEndpoint>();
        
        // People Group
        var peopleGroup = endpoints.MapGroup("v2/people")
            .WithTags("People");
        
        peopleGroup.MapGroup("Biller")
            .WithTags("People - biller")
            .MapEndpoint<CreateBillerEndpoint>()
            .MapEndpoint<UpdateBillerEndpoint>()
            .MapEndpoint<GetAllBillersEndpoint>()
            .MapEndpoint<DeleteBillerEndpoint>()
            .MapEndpoint<GetBillerByIdEndpoint>();
        
        peopleGroup.MapGroup("Customer")
            .WithTags("People - customer")
            .MapEndpoint<CreateCustomerEndpoint>()
            .MapEndpoint<UpdateCustomerEndpoint>()
            .MapEndpoint<GetAllCustomersEndpoint>()
            .MapEndpoint<DeleteCustomerEndpoint>()
            .MapEndpoint<GetCustomerByIdEndpoint>();
        
        peopleGroup.MapGroup("Supplier")
            .WithTags("People - supplier")
            .MapEndpoint<CreateSupplierEndpoint>()
            .MapEndpoint<UpdateSupplierEndpoint>()
            .MapEndpoint<GetAllSuppliersEndpoint>()
            .MapEndpoint<DeleteSupplierEndpoint>()
            .MapEndpoint<GetSupplierByIdEndpoint>();
        
        // Management Group
        var userManagementGroup = endpoints.MapGroup("v2/UserManagement")
            .WithTags("User Management"); // Fixed the tag here
        
        userManagementGroup.MapGroup("Role")
            .WithTags("UserManagement - role")
            .MapEndpoint<CreateRoleEndpoint>()
            .MapEndpoint<UpdateRoleEndpoint>()
            .MapEndpoint<GetAllRolesEndpoint>()
            .MapEndpoint<DeleteRoleEndpoint>()
            .MapEndpoint<GetRoleByIdEndpoint>();
        
        userManagementGroup.MapGroup("User")
            .WithTags("UserManagement - user")
            .MapEndpoint<CreateUserEndpoint>()
            .MapEndpoint<UpdateUserEndpoint>()
            .MapEndpoint<GetAllUsersEndpoint>()
            .MapEndpoint<DeleteUserEndpoint>()
            .MapEndpoint<GetUserByIdEndpoint>();
        
        // WAREHOUSE GROUP
        var warehouseGroup = endpoints.MapGroup("v2/Warehouse")
            .WithTags("Warehouse");
        
        warehouseGroup.MapGroup("Warehouse")
            .WithTags("Warehouse")
            .MapEndpoint<CreateWarehouseEndpoint>()
            .MapEndpoint<UpdateWarehouseEndpoint>()
            .MapEndpoint<GetAllWarehousesEndpoint>()
            .MapEndpoint<DeleteWarehouseEndpoint>()
            .MapEndpoint<GetWarehouseByIdEndpoint>();
    }

    private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
        where TEndpoint : IEndPoint
    {
        TEndpoint.Map(app);
        return app;
    }
}