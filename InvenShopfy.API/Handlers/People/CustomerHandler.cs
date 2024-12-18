using System.Globalization;
using InvenShopfy.API.Data;
using InvenShopfy.Core;
using InvenShopfy.Core.Enum;
using InvenShopfy.Core.Handlers.People;
using InvenShopfy.Core.Models.People;
using InvenShopfy.Core.Models.People.Dto;
using InvenShopfy.Core.Requests.People.Customer;
using InvenShopfy.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace InvenShopfy.API.Handlers.People;

public class CustomerHandler(AppDbContext context) : ICustomerHandler
{
    public async Task<Response<Customer?>> CreateCustomerAsync(CreateCustomerRequest request)
    {
        try
        {
            if (!request.UserHasPermission)
            {
                return new Response<Customer?>(null, 409, $"{Configuration.NotAuthorized} 'create'");
            }
            
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            var existingCustomer = await context.Customers
                .FirstOrDefaultAsync(
                    x => x.CustomerName.ToLower() == request.CustomerName.ToLower() || x.Email.ToLower() == request.Email.ToLower());

            if (existingCustomer != null)
            {
                if (existingCustomer.CustomerName.ToLower() == request.CustomerName.ToLower())
                {
                    return new Response<Customer?>(null, 409, $"The customer name '{request.CustomerName}' already exists.");
                }

                if (existingCustomer.Email.ToLower() == request.Email.ToLower())
                {
                    return new Response<Customer?>(null, 409, $"The customer email '{request.Email}' already exists.");
                }
            }

            var customer = new Customer
            {
                UserId = request.UserId,
                CustomerName = textInfo.ToTitleCase(request.CustomerName),
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                City = textInfo.ToTitleCase(request.City),
                Country = textInfo.ToTitleCase(request.Country),
                Address = textInfo.ToTitleCase(request.Address),
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

    public async Task<Response<Customer?>> UpdateCustomerAsync(UpdateCustomerRequest request)
    {
        try
        {
            var customer =
                await context.Customers.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (customer is null)
            {
                return new Response<Customer?>(null, 404, "Customer not found");
            }

            if (!Enum.IsDefined(typeof(ECustomerGroup), request.CustomerGroup))
            {
                return new Response<Customer?>(null, 400, "Invalid Customer Group");
            }

            customer.CustomerName = request.CustomerName;
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

    public async Task<Response<Customer?>> DeleteCustomerAsync(DeleteCustomerRequest request)
    {
        try
        {
            if (!request.UserHasPermission)
            {
                return new Response<Customer?>(null, 400, $"{Configuration.NotAuthorized} 'Delete'");
            }
            
            var customer =
                await context.Customers.FirstOrDefaultAsync(x => x.Id == request.Id);

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

    public async Task<Response<Customer?>> GetCustomerByIdAsync(GetCustomerByIdRequest request)
    {
        try
        {
            var customer = await context.Customers.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id);

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

    public async Task<PagedResponse<List<Customer>?>> GetCustomerByPeriodAsync(GetAllCustomersRequest request)
    {
        try
        {
            if (!request.UserHasPermission)
            {
                return new PagedResponse<List<Customer>?>([], 201, $"{Configuration.NotAuthorized}");
            }
            
            var query = context
                .Customers
                .AsNoTracking()
                .OrderBy(x => x.CustomerName);

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

    public async Task<PagedResponse<List<CustomerName>?>> GetCustomerNameAsync(GetAllCustomersRequest request)
    {
        try
        {
            var query = context
                .Customers
                .AsNoTracking()
                .Select(x => new CustomerName
                {
                    Id = x.Id,
                    Name = x.CustomerName
                })
                .OrderBy(x => x.Name);

            var customer = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            var count = await query.CountAsync();

            return new PagedResponse<List<CustomerName>?>(
                customer,
                count,
                request.PageNumber,
                request.PageSize);
        }
        catch
        {
            return new PagedResponse<List<CustomerName>?>(null, 500,
                "It was not possible to consult all customer names");
        }
    }
}