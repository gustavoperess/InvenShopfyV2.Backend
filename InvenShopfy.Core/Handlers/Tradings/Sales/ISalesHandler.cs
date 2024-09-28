using InvenShopfy.Core.Requests.Tradings.Sales;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.Core.Handlers.Tradings.Sales;

public interface ISalesHandler
{
    Task<Response<Models.Tradings.Sales.Sale?>> CreateAsync(CreateSalesRequest request);
    Task<Response<Models.Tradings.Sales.Sale?>> UpdateAsync(UpdateSalesRequest request);
    Task<Response<Models.Tradings.Sales.Sale?>> DeleteAsync(DeleteSalesRequest request);
    Task<Response<Models.Tradings.Sales.Sale?>> GetByIdAsync(GetSalesByIdRequest request);
    Task<PagedResponse<List<Models.Tradings.Sales.Sale>?>> GetByPeriodAsync(GetAllSalesRequest request);
    
    Task<PagedResponse<List<Models.Tradings.Sales.BestSeller>?>> GetByBestSeller(GetSalesByBestSeller request);
    Task<Response<Models.Tradings.Sales.Sale?>> GetSalesBySeller(GetSalesBySeller request);
    
    Task<Response<double?>> GetTotalAmountSalesRequestAsync(GetTotalSalesAmountRequest request);
    
}