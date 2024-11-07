using InvenShopfy.API.Common.Api;
using InvenShopfy.API.EndPoints.Expenses.Category;
using InvenShopfy.API.EndPoints.Expenses.Expense;
using InvenShopfy.API.EndPoints.Identity;
using InvenShopfy.API.EndPoints.People.Biller;
using InvenShopfy.API.EndPoints.People.Customer;
using InvenShopfy.API.EndPoints.People.Supplier;
using InvenShopfy.API.EndPoints.Products.Brands;
using InvenShopfy.API.EndPoints.Products.Categories;
using InvenShopfy.API.EndPoints.Products.Product;
using InvenShopfy.API.EndPoints.Products.Units;
using InvenShopfy.API.EndPoints.Reports.Sales;
using InvenShopfy.API.EndPoints.Tradings.Purchase.Add;
using InvenShopfy.API.EndPoints.Tradings.Returns.PurchaseReturn;
using InvenShopfy.API.EndPoints.Tradings.Returns.SalesReturn;
using InvenShopfy.API.EndPoints.Tradings.Sales;
using InvenShopfy.API.EndPoints.Transfer;
using InvenShopfy.API.EndPoints.Warehouses;
using InvenShopfy.API.Models;



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
            .RequireAuthorization()
            .MapEndpoint<CreateProductEndpoint>()
            .MapEndpoint<UpdateProductEndpoint>()
            .MapEndpoint<GetAllProductsEndpoint>()
            .MapEndpoint<GetProductByNameEndpoint>()
            .MapEndpoint<DeleteProductEndpoint>()
            .MapEndpoint<GetProductByIdEndpoint>();

        productsGroup.MapGroup("Brands")
            .WithTags("Products - brands")
            .RequireAuthorization()
            .MapEndpoint<CreateBrandEndpoint>()
            .MapEndpoint<UpdateBrandEndpoint>()
            .MapEndpoint<GetAllBrandsEndpoint>()
            .MapEndpoint<DeleteBrandEndpoint>()
            .MapEndpoint<GetBrandByIdEndpoint>();

        productsGroup.MapGroup("Units")
            .WithTags("Products - units")
            .RequireAuthorization()
            .MapEndpoint<CreateUnitEndpoint>()
            .MapEndpoint<UpdateUnitEndpoint>()
            .MapEndpoint<GetAllUnitsEndpoint>()
            .MapEndpoint<DeleteUnitEndpoint>()
            .MapEndpoint<GetUnitByIdEndpoint>();

        productsGroup.MapGroup("ProductCategory")
            .WithTags("Products - categories")
            .RequireAuthorization()
            .MapEndpoint<CreateCategoryEndpoint>()
            .MapEndpoint<UpdateCategoryEndpoint>()
            .MapEndpoint<GetAllCategoriesEndpoint>()
            .MapEndpoint<DeleteCategoryEndpoint>()
            .MapEndpoint<GetCategoryByIdEndpoint>();
        
        // Expense Group
        var expenseGroup = endpoints.MapGroup("v2")
            .WithTags("Expenses");
        
        expenseGroup.MapGroup("Expenses")
            .WithTags("Expenses - expenses")
            .RequireAuthorization()
            .MapEndpoint<CreateExpenseEndpoint>()
            .MapEndpoint<UpdateExpenseEndpoint>()
            .MapEndpoint<GetAllExpensesEndpoint>()
            .MapEndpoint<DeleteExpenseEndpoint>()
            .MapEndpoint<GetExpenseByIdEndpoint>();

        expenseGroup.MapGroup("ExpenseCategory")
            .WithTags("Expenses - category")
            .RequireAuthorization()
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
            .RequireAuthorization()
            .MapEndpoint<CreateBillerEndpoint>()
            .MapEndpoint<UpdateBillerEndpoint>()
            .MapEndpoint<GetAllBillersEndpoint>()
            .MapEndpoint<DeleteBillerEndpoint>()
            .MapEndpoint<GetBillerByIdEndpoint>();
        
        peopleGroup.MapGroup("Customer")
            .WithTags("People - customer")
            .RequireAuthorization()
            .MapEndpoint<CreateCustomerEndpoint>()
            .MapEndpoint<UpdateCustomerEndpoint>()
            .MapEndpoint<GetAllCustomersEndpoint>()
            .MapEndpoint<DeleteCustomerEndpoint>()
            .MapEndpoint<GetCustomerByIdEndpoint>();
        
        peopleGroup.MapGroup("Supplier")
            .WithTags("People - supplier")
            .RequireAuthorization()
            .MapEndpoint<CreateSupplierEndpoint>()
            .MapEndpoint<UpdateSupplierEndpoint>()
            .MapEndpoint<GetAllSuppliersEndpoint>()
            .MapEndpoint<DeleteSupplierEndpoint>()
            .MapEndpoint<GetSupplierByIdEndpoint>();
        
        // WAREHOUSE GROUP
        var warehouseGroup = endpoints.MapGroup("v2")
            .WithTags("Warehouse");
        
        warehouseGroup.MapGroup("Warehouse")
            .WithTags("Warehouse")
            .RequireAuthorization()
            .MapEndpoint<CreateWarehouseEndpoint>()
            .MapEndpoint<UpdateWarehouseEndpoint>()
            .MapEndpoint<GetAllWarehousesEndpoint>()
            .MapEndpoint<DeleteWarehouseEndpoint>()
            .MapEndpoint<GetWarehouseQuantityEndpoint>()
            .MapEndpoint<GetWarehouseByIdEndpoint>();
        
        // Purchase GROUP
        var purchaseGroup = endpoints.MapGroup("v2")
            .WithTags("Purchase");
        
        purchaseGroup.MapGroup("Purchase")
            .WithTags("Purchase")
            .RequireAuthorization()
            .MapEndpoint<CreatePurchaseEndpoint>()
            .MapEndpoint<UpdatePurchaseEndpoint>()
            .MapEndpoint<GetAllPurchasesEndpoint>()
            .MapEndpoint<DeletePurchaseEndpoint>()
            .MapEndpoint<GetPurchaseByIdEndpoint>();
        
        // Sales GROUP
        var salesGroup = endpoints.MapGroup("v2")
            .WithTags("Sale");

        salesGroup.MapGroup("Sale")
            .WithTags("Sale")
            .RequireAuthorization()
            .MapEndpoint<CreateSaleEndpoint>()
            .MapEndpoint<UpdateSalesEndpoint>()
            .MapEndpoint<GetAllSalesEndpoint>()
            .MapEndpoint<DeleteSaleEndpoint>()
            .MapEndpoint<GetSalesBySalesIdEndpoint>()
            .MapEndpoint<GetTotalAmountSalesEndpoint>()
            .MapEndpoint<GetSalesByBestSellerEndpoint>()
            .MapEndpoint<GetMostSouldProductEndpoint>();
    
        
        
        // SalesReport GROUP
        var salesReportGroup = endpoints.MapGroup("v2")
            .WithTags("SaleReport");

        salesReportGroup.MapGroup("SalesReport")
            .WithTags("SalesReport")
            .RequireAuthorization()
            .MapEndpoint<GetSalesReportByDateEndpoint>()
            .MapEndpoint<GetSalesReportByWarehouseNameEndpoint>();
        
        
        // SalesReturn GROUP
        var salesReturnGroup = endpoints.MapGroup("v2")
            .WithTags("SalesReturn");

        salesReturnGroup.MapGroup("SalesReturn")
            .WithTags("SalesReturn")
            .RequireAuthorization()
            .MapEndpoint<CreateSalesReturnEndpoint>()
            .MapEndpoint<GetSalesReturnByReturnNumberEndpoint>()
            .MapEndpoint<DeleteSalesReturnEndpoint>()
            .MapEndpoint<GetAllSalesReturnEndoint>();
        
        
        // PurchaseReturn GROUP
        var purchaseReturnGroup = endpoints.MapGroup("v2")
            .WithTags("PurchasesReturn");

        purchaseReturnGroup.MapGroup("PurchasesReturn")
            .WithTags("PurchasesReturn")
            .RequireAuthorization()
            .MapEndpoint<CreatePurchaseReturnEndpoin>()
            .MapEndpoint<GetAllPurchaseReturnsEndpoint>()
            .MapEndpoint<DeletePurchaseReturnEndpoint>()
            .MapEndpoint<GetPurchaseByReturnNumberEndpoint>();
        
        // PurchaseReturn GROUP
        var transferGroup = endpoints.MapGroup("v2")
            .WithTags("Transfer");

        transferGroup.MapGroup("Transfer")
            .WithTags("Transfer")
            .RequireAuthorization()
            .MapEndpoint<CreateTransferEndpoint>();
     

        
        // Management Group
        endpoints.MapGroup("v2/identity")
            .WithTags("Identity")
            .MapIdentityApi<CustomUserRequest>();

        endpoints.MapGroup("v2/identity")
            .WithTags("Identity")
            .MapEndpoint<LogoutEndpoint>()
            .MapEndpoint<RegisterEndpoint>()
            .MapEndpoint<GetIdentityUsersEndpoint>()
            .MapEndpoint<DeleteRoleIdentityEndpoint>()
            .MapEndpoint<GetIdentityRolesEndpoint>()
            .MapEndpoint<CreateRoleIdentityEndpoint>()
            .MapEndpoint<LoginEndpointEndpoint>(); 
    }

    private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
        where TEndpoint : IEndPoint
    {
        TEndpoint.Map(app);
        return app;
    }
}