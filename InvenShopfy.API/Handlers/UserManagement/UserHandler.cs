using InvenShopfy.API.Data;
using InvenShopfy.Core.Enum;
using InvenShopfy.Core.Handlers.UserManagement;
using InvenShopfy.Core.Models.UserManagement;
using InvenShopfy.Core.Requests.UserManagement.User;
using InvenShopfy.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace InvenShopfy.API.Handlers.UserManagement;

// public class UserHandler (AppDbContext context) : IUserManagementUserHandler
// {
//     public async Task<Response<User?>> CreateAsync(CreateUserRequest request)
//     {
//         try
//         {
//             if (!Enum.IsDefined(typeof(EGender), request.Gender))
//             {
//                 return new Response<User?>(null, 400, "Gender invalid");
//             }
//             var user = new User
//             {
//                 UserId = request.UserId,
//                 Name = request.Name,
//                 Email = request.UserId,
//                 PhoneNumber = request.UserId,
//                 Gender = request.Gender, 
//                 Username = request.Username,
//                 ProfileImage = request.ProfileImage,
//                 Password = request.Password,
//                 RoleId = request.RoleId,
//             };
//             await context.Users.AddAsync(user);
//             await context.SaveChangesAsync();
//
//             return new Response<User?>(user, 201, "User created successfully");
//
//         }
//         catch
//         {
//             return new Response<User?>(null, 500, "It was not possible to create a new user");
//         }
//     }
//
//     public async Task<Response<User?>> UpdateAsync(UpdateUserRequest request)
//     {
//         try
//         {
//             var user = await context.Users.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
//
//             if (user is null)
//             {
//                 return new Response<User?>(null, 404, "User not found");
//             }
//             
//             if (!Enum.IsDefined(typeof(EGender), request.Gender))
//             {
//                 return new Response<User?>(null, 400, "Gender invalid");
//             }
//             
//             user.Name = request.Name;
//             user.Email = request.UserId;
//             user.PhoneNumber = request.UserId;
//             user.Gender = request.Gender;
//             user.Username = request.Username;
//             user.ProfileImage = request.ProfileImage;
//             user.Password = request.Password;
//             user.RoleId = request.RoleId;
//             context.Users.Update(user);
//             await context.SaveChangesAsync();
//             return new Response<User?>(user, message: "User updated successfully");
//
//         }
//         catch 
//         {
//             return new Response<User?>(null, 500, "It was not possible to update this user");
//         }
//     }
//
//     public async Task<Response<User?>> DeleteAsync(DeleteUserRequest request)
//     {
//         try
//         {
//             var user = await context.Users.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
//             
//             if (user is null)
//             {
//                 return new Response<User?>(null, 404, "User not found");
//             }
//
//             context.Users.Remove(user);
//             await context.SaveChangesAsync();
//             return new Response<User?>(user, message: "User removed successfully");
//
//         }
//         catch 
//         {
//             return new Response<User?>(null, 500, "It was not possible to delete this user");
//         }
//     }
//     
//     public async Task<Response<User?>> GetByIdAsync(GetUserByIdRequest byIdRequest)
//     {
//         try
//         {
//             var user = await context.Users.FirstOrDefaultAsync(x => x.Id == byIdRequest.Id && x.UserId == byIdRequest.UserId);
//             
//             if (user is null)
//             {
//                 return new Response<User?>(null, 404, "User not found");
//             }
//
//             return new Response<User?>(user);
//
//         }
//         catch 
//         {
//             return new Response<User?>(null, 500, "It was not possible to find this user");
//         }
//     }
//     public async Task<PagedResponse<List<User>?>> GetByPeriodAsync(GetAllUsersRequest request)
//     {
//         try
//         {
//             var query = context
//                 .Users
//                 .AsNoTracking()
//                 .Where(x => x.UserId == request.UserId)
//                 .OrderBy(x => x.Name);
//             
//             var user = await query
//                 .Skip((request.PageNumber - 1) * request.PageSize)
//                 .Take(request.PageSize)
//                 .ToListAsync();
//             
//             var count = await query.CountAsync();
//             
//             return new PagedResponse<List<User>?>(
//                 user,
//                 count,
//                 request.PageNumber,
//                 request.PageSize);
//         }
//         catch 
//         {
//             return new PagedResponse<List<User>?>(null, 500, "It was not possible to consult all user");
//         }
//     }
// }