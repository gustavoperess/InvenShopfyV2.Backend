using InvenShopfy.API.Data;
using InvenShopfy.Core.Enum;
using InvenShopfy.Core.Handlers.People;
using InvenShopfy.Core.Models.People;
using InvenShopfy.Core.Requests.People.Customer;
using InvenShopfy.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace InvenShopfy.API.Handlers.People;

public class CustomerHandler (AppDbContext context) : ICustomerHandler
{
    public async Task<Response<Customer?>> CreateAsync(CreateCustomerRequest request)
    {
        try
        {
            if (!Enum.IsDefined(typeof(ECustomerGroup), request.CustomerGroup))
            {
                return new Response<Customer?>(null, 400, "Invalid Customer Group");
            }
            
            var customer = new Customer
            {
                UserId = request.UserId,
                Name = request.Name,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                City = request.City,
                Country = request.Country,
                Address = request.Address,
                ZipCode = request.ZipCode,
                RewardPoint = request.RewardPoint,
                CustomerGroup = request.CustomerGroup,
            };
            await context.Customers.AddAsync(customer);
            await context.SaveChangesAsync();

            return new Response<Customer?>(customer, 201, "Customer created successfully");

        }
        catch
        {
            return new Response<Customer?>(null, 500, "It was not possible to create a new customer");
        }
    }

    public async Task<Response<Customer?>> UpdateAsync(UpdateCustomerRequest request)
    {
        try
        {
            var customer = await context.Customers.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (customer is null)
            {
                return new Response<Customer?>(null, 404, "Customer not found");
            }

            customer.Name = request.Name;
            customer.Email = request.Email;
            customer.PhoneNumber = request.PhoneNumber;
            customer.City = request.City;
            customer.Country = request.Country;
            customer.Address = request.Address;
            customer.ZipCode = request.ZipCode;
            customer.RewardPoint = request.RewardPoint;
            customer.CustomerGroup = request.CustomerGroup;
            
            context.Customers.Update(customer);
            await context.SaveChangesAsync();
            return new Response<Customer?>(customer, message: "Customer updated successfully");

        }
        catch 
        {
            return new Response<Customer?>(null, 500, "It was not possible to update this Customer");
        }
    }

    public async Task<Response<Customer?>> DeleteAsync(DeleteCustomerRequest request)
    {
        try
        {
            var customer = await context.Customers.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
            
            if (customer is null)
            {
                return new Response<Customer?>(null, 404, "Customer not found");
            }

            context.Customers.Remove(customer);
            await context.SaveChangesAsync();
            return new Response<Customer?>(customer, message: "Customer removed successfully");

        }
        catch 
        {
            return new Response<Customer?>(null, 500, "It was not possible to delete this customer");
        }
    }
    
    public async Task<Response<Customer?>> GetByIdAsync(GetCustomerByIdRequest request)
    {
        try
        {
            var customer = await context.Customers.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
            
            if (customer is null)
            {
                return new Response<Customer?>(null, 404, "Customer not found");
            }

            return new Response<Customer?>(customer);

        }
        catch 
        {
            return new Response<Customer?>(null, 500, "It was not possible to find this customer");
        }
    }
    public async Task<PagedResponse<List<Customer>?>> GetByPeriodAsync(GetAllCustomersRequest request)
    {
        try
        {
            var query = context
                .Customers
                .AsNoTracking()
                .Where(x => x.UserId == request.UserId)
                .OrderBy(x => x.Name);
            
            var customer = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
            
            var count = await query.CountAsync();
            
            return new PagedResponse<List<Customer>?>(
                customer,
                count,
                request.PageNumber,
                request.PageSize);
        }
        catch 
        {
            return new PagedResponse<List<Customer>?>(null, 500, "It was not possible to consult all customer");
        }
    }
}