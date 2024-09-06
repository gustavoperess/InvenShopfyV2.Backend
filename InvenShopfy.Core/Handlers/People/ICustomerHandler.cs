using InvenShopfy.Core.Requests.People.Customer;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.Core.Handlers.People;

public interface ICustomerHandler
{
    Task<Response<Models.People.Customer?>> CreateAsync(CreateCustomerRequest request);
    Task<Response<Models.People.Customer?>> UpdateAsync(UpdateCustomerRequest request);
    Task<Response<Models.People.Customer?>> DeleteAsync(DeleteCustomerRequest request);
    Task<Response<Models.People.Customer?>> GetByIdAsync(GetCustomerByIdRequest request);
    Task<PagedResponse<List<Models.People.Customer>?>> GetByPeriodAsync(GetAllCustomersRequest request);
}