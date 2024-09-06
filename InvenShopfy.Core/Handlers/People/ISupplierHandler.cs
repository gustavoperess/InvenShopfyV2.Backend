using InvenShopfy.Core.Requests.People.Supplier;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.Core.Handlers.People;

public interface ISupplierHandler
{
    Task<Response<Models.People.Supplier?>> CreateAsync(CreateSupplierRequest request);
    Task<Response<Models.People.Supplier?>> UpdateAsync(UpdateSupplierRequest request);
    Task<Response<Models.People.Supplier?>> DeleteAsync(DeleteSupplierRequest request);
    Task<Response<Models.People.Supplier?>> GetByIdAsync(GetSupplierByIdRequest request);
    Task<PagedResponse<List<Models.People.Supplier>?>> GetByPeriodAsync(GetAllSuppliersRequest request);
}