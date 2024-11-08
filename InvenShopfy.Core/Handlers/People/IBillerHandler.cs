using InvenShopfy.Core.Models.People;
using InvenShopfy.Core.Models.People.Dto;
using InvenShopfy.Core.Requests.People.Biller;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.Core.Handlers.People;

public interface IBillerHandler
{
    Task<Response<Biller?>> CreateBillerAsync(CreateBillerRequest request);
    Task<Response<Biller?>> UpdateBillerAsync(UpdateBillerRequest request);
    Task<Response<Biller?>> DeleteBillerAsync(DeleteBillerRequest request);
    Task<Response<Biller?>> GetBillerByIdAsync(GetBillerByIdRequest request);
    Task<PagedResponse<List<BillerDto>?>> GetBillerByPeriodAsync(GetAllBillerRequest request);
    Task<PagedResponse<List<BillerName>?>> GetBillerNameAsync(GetAllBillerRequest request);
}