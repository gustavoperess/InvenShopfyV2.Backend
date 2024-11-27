using InvenShopfy.Core.Models.Tradings.Sales.SalesPayment;
using InvenShopfy.Core.Requests.Tradings.Sales.SalesPayment;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.Core.Handlers.Tradings.Sales;

public interface ISalesPaymentHandler
{
    Task<Response<SalesPayment?>> CreateSalesPaymentAsync(CreateSalesPaymentRequest request);
    
    Task<Response<SalesPaymentDto?>> GetSalesPaymentByIdAsync(GetSalesPaymentByIdRequest request);
    
}