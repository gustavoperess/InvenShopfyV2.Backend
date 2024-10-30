using InvenShopfy.Core.Requests.People.Customer;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.Core.Handlers.People;

public interface ICustomerHandler
{
    Task<Response<Models.People.Customer?>> CreateCustomerAsync(CreateCustomerRequest request);
    Task<Response<Models.People.Customer?>> UpdateCustomerAsync(UpdateCustomerRequest request);
    Task<Response<Models.People.Customer?>> DeleteCustomerAsync(DeleteCustomerRequest request);
    Task<Response<Models.People.Customer?>> GetCustomerByIdAsync(GetCustomerByIdRequest request);
    Task<PagedResponse<List<Models.People.Customer>?>> GetCustomerByPeriodAsync(GetAllCustomersRequest request);
}