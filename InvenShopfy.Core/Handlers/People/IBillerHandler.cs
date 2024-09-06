using InvenShopfy.Core.Requests.People.Biller;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.Core.Handlers.People;

public interface IBillerHandler
{
    Task<Response<Models.People.Biller?>> CreateAsync(CreateBillerRequest request);
    Task<Response<Models.People.Biller?>> UpdateAsync(UpdateBillerRequest request);
    Task<Response<Models.People.Biller?>> DeleteAsync(DeleteBillerRequest request);
    Task<Response<Models.People.Biller?>> GetByIdAsync(GetBillerByIdRequest request);
    Task<PagedResponse<List<Models.People.Biller>?>> GetByPeriodAsync(GetAllBillerRequest request);
}