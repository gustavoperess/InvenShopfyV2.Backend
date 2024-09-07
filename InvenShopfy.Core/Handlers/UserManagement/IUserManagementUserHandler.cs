using InvenShopfy.Core.Requests.UserManagement.Role;
using InvenShopfy.Core.Requests.UserManagement.User;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.Core.Handlers.UserManagement;

public interface IUserManagementUserHandler
{
    Task<Response<Models.UserManagement.User?>> CreateAsync(CreateUserRequest request);
    Task<Response<Models.UserManagement.User?>> UpdateAsync(UpdateUserRequest request);
    Task<Response<Models.UserManagement.User?>> DeleteAsync(DeleteUserRequest request);
    Task<Response<Models.UserManagement.User?>> GetByIdAsync(GetUserByIdRequest byIdRequest);
    Task<PagedResponse<List<Models.UserManagement.User>?>> GetByPeriodAsync(GetAllUsersRequest request);
}