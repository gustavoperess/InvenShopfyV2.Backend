using InvenShopfy.API.Data;
using InvenShopfy.Core.Handlers.People;
using InvenShopfy.Core.Models.People;
using InvenShopfy.Core.Requests.People.Biller;
using InvenShopfy.Core.Responses;
using Microsoft.EntityFrameworkCore;


namespace InvenShopfy.API.Handlers.People;

public class BillerHandler (AppDbContext context) : IBillerHandler
{
    public async Task<Response<Biller?>> CreateAsync(CreateBillerRequest request)
    {
        try
        {
            var biller = new Biller
            {
                UserId = request.UserId,
                Name = request.Name,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Identification = request.Identification,
                DateOfJoin = request.DateOfJoin,
                Address = request.Address,
                Country = request.Country,
                ZipCode = request.ZipCode,
                BillerCode = request.BillerCode,
                WarehouseId = request.WarehouseId,
            };
            
            await context.Billers.AddAsync(biller);
            await context.SaveChangesAsync();

            return new Response<Biller?>(biller, 201, "Biller created successfully");

        }
        catch
        {
            return new Response<Biller?>(null, 500, $"It was not possible to create a new Biller ");
        }
    }

    public async Task<Response<Biller?>> UpdateAsync(UpdateBillerRequest request)
    {
        try
        {
            var biller = await context.Billers.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (biller is null)
            {
                return new Response<Biller?>(null, 404, "Biller not found");
            }

            biller.UserId = request.UserId;
            biller.Name = request.Name;
            biller.Email = request.Email;
            biller.PhoneNumber = request.PhoneNumber;
            biller.Identification = request.Identification;
            biller.Address = request.Address;
            biller.Country = request.Country;
            biller.ZipCode = request.ZipCode;
            biller.BillerCode = request.BillerCode;
            biller.WarehouseId = request.WarehouseId;
            context.Billers.Update(biller);
            await context.SaveChangesAsync();
            return new Response<Biller?>(biller, message: "Biller updated successfully");

        }
        catch 
        {
            return new Response<Biller?>(null, 500, "It was not possible to update this Biller");
        }
    }

    public async Task<Response<Biller?>> DeleteAsync(DeleteBillerRequest request)
    {
        try
        {
            var biller = await context.Billers.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
            
            if (biller is null)
            {
                return new Response<Biller?>(null, 404, "Biller not found");
            }

            context.Billers.Remove(biller);
            await context.SaveChangesAsync();
            return new Response<Biller?>(biller, message: "Biller removed successfully");

        }
        catch 
        {
            return new Response<Biller?>(null, 500, "It was not possible to delete this Biller");
        }
    }
    
    public async Task<Response<Biller?>> GetByIdAsync(GetBillerByIdRequest request)
    {
        try
        {
            var biller = await context.Billers.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
            
            if (biller is null)
            {
                return new Response<Biller?>(null, 404, "Biller not found");
            }

            return new Response<Biller?>(biller);

        }
        catch 
        {
            return new Response<Biller?>(null, 500, "It was not possible to find this Biller");
        }
    }
    public async Task<PagedResponse<List<Biller>?>> GetByPeriodAsync(GetAllBillerRequest request)
    {
        try
        {
            var query = context
                .Billers
                .AsNoTracking()
                .Where(x => x.UserId == request.UserId)
                .OrderBy(x => x.WarehouseId);
            
            var biller = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
            
            var count = await query.CountAsync();
            
            return new PagedResponse<List<Biller>?>(
                biller,
                count,
                request.PageNumber,
                request.PageSize);
        }
        catch 
        {
            return new PagedResponse<List<Biller>?>(null, 500, "It was not possible to consult all Billers");
        }
    }
}