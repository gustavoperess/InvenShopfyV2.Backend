using InvenShopfy.Core.Models.People.Dto;
using InvenShopfy.Core.Requests.People.Supplier;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.Core.Handlers.People;

public interface ISupplierHandler
{
    Task<Response<Models.People.Supplier?>> CreateSupplierAsync(CreateSupplierRequest request);
    Task<Response<Models.People.Supplier?>> UpdateSupplierAsync(UpdateSupplierRequest request);
    Task<Response<Models.People.Supplier?>> DeleteSupplierAsync(DeleteSupplierRequest request);
    Task<Response<Models.People.Supplier?>> GetSupplierByIdAsync(GetSupplierByIdRequest request);
    Task<PagedResponse<List<Models.People.Supplier>?>> GetSupplierByPeriodAsync(GetAllSuppliersRequest request);
    Task<PagedResponse<List<SupplierName>?>> GetSupplierNameAsync(GetAllSuppliersRequest request);
}