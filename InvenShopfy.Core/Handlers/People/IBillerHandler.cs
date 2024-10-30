using InvenShopfy.Core.Requests.People.Biller;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.Core.Handlers.People;

public interface IBillerHandler
{
    Task<Response<Models.People.Biller?>> CreateBillerAsync(CreateBillerRequest request);
    Task<Response<Models.People.Biller?>> UpdateBillerAsync(UpdateBillerRequest request);
    Task<Response<Models.People.Biller?>> DeleteBillerAsync(DeleteBillerRequest request);
    Task<Response<Models.People.Biller?>> GetBillerByIdAsync(GetBillerByIdRequest request);
    Task<PagedResponse<List<Models.People.BillerDto>?>> GetBillerByPeriodAsync(GetAllBillerRequest request);
}