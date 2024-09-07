using InvenShopfy.API.Data;
using InvenShopfy.Core.Handlers.People;
using InvenShopfy.Core.Models.People;
using InvenShopfy.Core.Requests.People.Supplier;
using InvenShopfy.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace InvenShopfy.API.Handlers.People;

public class SupplierHandler (AppDbContext context) : ISupplierHandler
{
      public async Task<Response<Supplier?>> CreateAsync(CreateSupplierRequest request)
    {
        try
        {
            var supplier = new Supplier
            {
                UserId = request.UserId,
                Name = request.Name,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                SupplierCode = request.SupplierCode,
                Country = request.Country,
                City = request.City,
                Address = request.Address,
                ZipCode = request.ZipCode,
                Company = request.Company,
            };
            await context.Suppliers.AddAsync(supplier);
            await context.SaveChangesAsync();

            return new Response<Supplier?>(supplier, 201, "Supplier created successfully");

        }
        catch
        {
            return new Response<Supplier?>(null, 500, "It was not possible to create a new supplier");
        }
    }

    public async Task<Response<Supplier?>> UpdateAsync(UpdateSupplierRequest request)
    {
        try
        {
            var supplier = await context.Suppliers.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (supplier is null)
            {
                return new Response<Supplier?>(null, 404, "supplier not found");
            }
            
            supplier.UserId = request.UserId;
            supplier.Name = request.Name;
            supplier.PhoneNumber = request.PhoneNumber;
            supplier.Email = request.Email;
            supplier.Country = request.Country;
            supplier.City = request.City;
            supplier.Address = request.Address;
            supplier.ZipCode = request.ZipCode;
            supplier.SupplierCode = request.SupplierCode;
            supplier.Company = request.Company;
            
            context.Suppliers.Update(supplier);
            await context.SaveChangesAsync();
            return new Response<Supplier?>(supplier, message: "Supplier updated successfully");

        }
        catch 
        {
            return new Response<Supplier?>(null, 500, "It was not possible to update this Supplier");
        }
    }

    public async Task<Response<Supplier?>> DeleteAsync(DeleteSupplierRequest request)
    {
        try
        {
            var supplier = await context.Suppliers.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
            
            if (supplier is null)
            {
                return new Response<Supplier?>(null, 404, "Supplier not found");
            }

            context.Suppliers.Remove(supplier);
            await context.SaveChangesAsync();
            return new Response<Supplier?>(supplier, message: "Supplier removed successfully");

        }
        catch 
        {
            return new Response<Supplier?>(null, 500, "It was not possible to delete this supplier");
        }
    }
    
    public async Task<Response<Supplier?>> GetByIdAsync(GetSupplierByIdRequest request)
    {
        try
        {
            var supplier = await context.Suppliers.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
            
            if (supplier is null)
            {
                return new Response<Supplier?>(null, 404, "Supplier not found");
            }

            return new Response<Supplier?>(supplier);

        }
        catch 
        {
            return new Response<Supplier?>(null, 500, "It was not possible to find this supplier");
        }
    }
    public async Task<PagedResponse<List<Supplier>?>> GetByPeriodAsync(GetAllSuppliersRequest request)
    {
        try
        {
            var query = context
                .Suppliers
                .AsNoTracking()
                .Where(x => x.UserId == request.UserId)
                .OrderBy(x => x.Name);
            
            var supplier = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
            
            var count = await query.CountAsync();
            
            return new PagedResponse<List<Supplier>?>(
                supplier,
                count,
                request.PageNumber,
                request.PageSize);
        }
        catch 
        {
            return new PagedResponse<List<Supplier>?>(null, 500, "It was not possible to consult all suppliers");
        }
    }
}