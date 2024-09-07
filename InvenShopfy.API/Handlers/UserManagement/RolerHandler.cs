using InvenShopfy.API.Data;
using InvenShopfy.Core.Handlers.UserManagement;
using InvenShopfy.Core.Models.UserManagement;
using InvenShopfy.Core.Requests.UserManagement.Role;
using InvenShopfy.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace InvenShopfy.API.Handlers.UserManagement;

public class RolerHandler (AppDbContext context) : IUserManagementRoleHandler
{
    public async Task<Response<Role?>> CreateAsync(CreateRoleRequest request)
    {
        try
        {
            var role = new Role
            {
                UserId = request.UserId,
                RoleName = request.RoleName,
                Description = request.Description
              
            };
            await context.Roles.AddAsync(role);
            await context.SaveChangesAsync();

            return new Response<Role?>(role, 201, "Role created successfully");

        }
        catch
        {
            return new Response<Role?>(null, 500, "It was not possible to create a new role");
        }
    }

    public async Task<Response<Role?>> UpdateAsync(UpdateRoleRequest request)
    {
        try
        {
            var role = await context.Roles.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (role is null)
            {
                return new Response<Role?>(null, 404, "Role not found");
            }
            
            role.RoleName = request.RoleName;
            role.Description = request.Description;
            context.Roles.Update(role);
            await context.SaveChangesAsync();
            return new Response<Role?>(role, message: "Role updated successfully");

        }
        catch 
        {
            return new Response<Role?>(null, 500, "It was not possible to update this Role");
        }
    }

    public async Task<Response<Role?>> DeleteAsync(DeleteRoleRequest request)
    {
        try
        {
            var role = await context.Roles.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
            
            if (role is null)
            {
                return new Response<Role?>(null, 404, "Role not found");
            }

            context.Roles.Remove(role);
            await context.SaveChangesAsync();
            return new Response<Role?>(role, message: "Role removed successfully");

        }
        catch 
        {
            return new Response<Role?>(null, 500, "It was not possible to delete this role");
        }
    }
    
    public async Task<Response<Role?>> GetByIdAsync(GetRoleByIdRequest request)
    {
        try
        {
            var role = await context.Roles.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
            
            if (role is null)
            {
                return new Response<Role?>(null, 404, "Role not found");
            }

            return new Response<Role?>(role);

        }
        catch 
        {
            return new Response<Role?>(null, 500, "It was not possible to find this role");
        }
    }
    public async Task<PagedResponse<List<Role>?>> GetByPeriodAsync(GetAllRolesRequest request)
    {
        try
        {
            var query = context
                .Roles
                .AsNoTracking()
                .Where(x => x.UserId == request.UserId)
                .OrderBy(x => x.RoleName);
            
            var role = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
            
            var count = await query.CountAsync();
            
            return new PagedResponse<List<Role>?>(
                role,
                count,
                request.PageNumber,
                request.PageSize);
        }
        catch 
        {
            return new PagedResponse<List<Role>?>(null, 500, "It was not possible to consult all Role");
        }
    }
}