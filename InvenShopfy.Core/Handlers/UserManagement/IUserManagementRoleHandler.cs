using InvenShopfy.Core.Requests.UserManagement.Role;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.Core.Handlers.UserManagement;

public interface IUserManagementRoleHandler
{
    Task<Response<Models.UserManagement.Role?>> CreateAsync(CreateRoleRequest request);
    Task<Response<Models.UserManagement.Role?>> UpdateAsync(UpdateRoleRequest request);
    Task<Response<Models.UserManagement.Role?>> DeleteAsync(DeleteRoleRequest request);
    Task<Response<Models.UserManagement.Role?>> GetByIdAsync(GetRoleByIdRequest request);
    Task<PagedResponse<List<Models.UserManagement.Role>?>> GetByPeriodAsync(GetAllRolesRequest request);
    
}