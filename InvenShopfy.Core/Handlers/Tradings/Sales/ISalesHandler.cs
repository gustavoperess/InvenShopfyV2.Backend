using InvenShopfy.Core.Requests.Tradings.Sales;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.Core.Handlers.Tradings.Sales;

public interface ISalesHandler
{
    Task<Response<Models.Tradings.Sales.Sale?>> CreateSaleAsync(CreateSalesRequest request);
    Task<Response<Models.Tradings.Sales.Sale?>> UpdateSaleAsync(UpdateSalesRequest request);
    Task<Response<Models.Tradings.Sales.Sale?>> DeleteSaleAsync(DeleteSalesRequest request);
    // Task<Response<Models.Tradings.Sales.Sale?>> GetSaleByIdAsync(GetSalesByIdRequest request);
    Task<PagedResponse<List<Models.Tradings.Sales.SaleList>?>> GetSaleByPeriodAsync(GetAllSalesRequest request);
    
    Task<PagedResponse<List<Models.Tradings.Sales.BestSeller>?>> GetByBestSellerAsync(GetSalesByBestSeller request);
    
    
    Task<Response<List<Models.Tradings.Sales.SalePerProduct>?>> GetSalesBySaleIdAsync(GetSalesBySaleIdRequest request);
    
    Task<PagedResponse<List<Models.Tradings.Sales.MostSoldProduct>?>> GetMostSoldProductAsync(GetMostSoldProduct request);
    Task<Response<Models.Tradings.Sales.Sale?>> GetSalesBySellerAsync(GetSalesBySeller request);
    
    Task<Response<decimal?>> GetTotalAmountSalesRequestAsync(GetTotalSalesAmountRequest request);
    
}