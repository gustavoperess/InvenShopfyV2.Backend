using InvenShopfy.API.Data;
using InvenShopfy.Core;
using InvenShopfy.Core.Handlers.People;
using InvenShopfy.Core.Models.People;
using InvenShopfy.Core.Models.People.Dto;
using InvenShopfy.Core.Requests.People.Biller;
using InvenShopfy.Core.Responses;
using Microsoft.EntityFrameworkCore;


namespace InvenShopfy.API.Handlers.People;

public class BillerHandler (AppDbContext context) : IBillerHandler
{
    public async Task<Response<Biller?>> CreateBillerAsync(CreateBillerRequest request)
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

    public async Task<Response<Biller?>> UpdateBillerAsync(UpdateBillerRequest request)
    {
        try
        {
            var biller = await context.Billers.FirstOrDefaultAsync(x => x.Id == request.Id);

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

    public async Task<Response<Biller?>> DeleteBillerAsync(DeleteBillerRequest request)
    {
        try
        {
            if (!request.UserHasPermission)
            {
                return new Response<Biller?>(null, 400, $"{Configuration.NotAuthorized} 'Delete'");
            }
            
            var biller = await context.Billers.FirstOrDefaultAsync(x => x.Id == request.Id);
            
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
    
    public async Task<Response<Biller?>> GetBillerByIdAsync(GetBillerByIdRequest request)
    {
        try
        {
            var biller = await context.Billers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id);
            
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
    public async Task<PagedResponse<List<BillerDto>?>> GetBillerByPeriodAsync(GetAllBillerRequest request)
    {
        try
        {
            if (!request.UserHasPermission)
            {
                return new PagedResponse<List<BillerDto>?>([], 201, $"{Configuration.NotAuthorized}");
            }
            
            var query = context
                .Billers
                .AsNoTracking()
                .Include(x => x.Warehouse)
                .GroupBy(x => new
                {
                    x.PhoneNumber, x.Address, x.Id, x.Country, x.Email, x.Identification,
                    x.Name, x.Warehouse.WarehouseName, x.ZipCode, x.DateOfJoin, x.BillerCode
                })
                .Select(g => new
                {
                    g.Key.Id,
                    g.Key.PhoneNumber,
                    g.Key.Address,
                    g.Key.Country,
                    g.Key.Email,
                    g.Key.Identification,
                    g.Key.Name,
                    g.Key.WarehouseName,
                    g.Key.ZipCode,
                    g.Key.DateOfJoin,
                    g.Key.BillerCode
                });
            
            var biller = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            var result = biller.Select(s => new BillerDto
            {
                Id = s.Id,
                PhoneNumber = s.PhoneNumber,
                Address = s.Address,
                Country = s.Country,
                Email = s.Email,
                Identification = s.Identification,
                Name = s.Name,
                WarehouseName = s.WarehouseName,
                ZipCode = s.ZipCode,
                DateOfJoin = s.DateOfJoin,
                BillerCode = s.BillerCode
            }).ToList();
            
            var count = await query.CountAsync();
            
            return new PagedResponse<List<BillerDto>?>(result, count,
                request.PageNumber,
                request.PageSize);
        }
        catch 
        {
            return new PagedResponse<List<BillerDto>?>(null, 500, "It was not possible to consult all Billers");
        }
    }
    
    public async Task<PagedResponse<List<BillerName>?>> GetBillerNameAsync(GetAllBillerRequest request)
    {
        try
        {
            var query = context
                .Billers
                .AsNoTracking()
                .Select(x => new BillerName
                { 
                    Id = x.Id,
                    Name = x.Name
                    
                })
                .OrderBy(x => x.Name);
            
            var biller = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
            
            var count = await query.CountAsync();
            
            return new PagedResponse<List<BillerName>?>(
                biller,
                count,
                request.PageNumber,
                request.PageSize);
        }
        catch 
        {
            return new PagedResponse<List<BillerName>?>(null, 500, "It was not possible to consult all biller names");
        }
    }
}