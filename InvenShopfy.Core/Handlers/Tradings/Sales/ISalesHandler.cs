using InvenShopfy.Core.Models.Tradings.Sales.Dto;
using InvenShopfy.Core.Requests.Tradings.Sales;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.Core.Handlers.Tradings.Sales;

public interface ISalesHandler
{
    Task<Response<Models.Tradings.Sales.Sale?>> CreateSaleAsync(CreateSalesRequest request);
    Task<Response<Models.Tradings.Sales.Sale?>> UpdateSaleAsync(UpdateSalesRequest request);
    Task<Response<Models.Tradings.Sales.Sale?>> DeleteSaleAsync(DeleteSalesRequest request);
    Task<PagedResponse<List<SaleList>?>> GetSaleByPeriodAsync(GetAllSalesRequest request);
    Task<Response<List<SalePopUp>?>> GetSalesBySaleIdAsync(GetSalesBySaleIdRequest request);
    
    Task<Response<List<PosSale>?>> GetSalesBySaleIdForPosSaleAsync(GetSalesBySaleIdRequest request);
    Task<PagedResponse<List<MostSoldProduct>?>> GetMostSoldProductAsync(GetMostSoldProduct request);
    Task<Response<List<SallerDashboard>?>> GetSaleStatusDashboardAsync(GetAllSalesRequest request);
    Task<Response<decimal?>> GetTotalAmountSalesRequestAsync();
    Task<Response<decimal>> GetTotalProfitDashboardAsync();
    
    

}