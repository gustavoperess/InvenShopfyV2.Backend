using System.Security.Claims;
using InvenShopfy.API.Common.Api;

namespace InvenShopfy.API.EndPoints.Identity;

public class GetRolesEndpoint : IEndPoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/roles", Handle).RequireAuthorization();
    
        private static Task<IResult> Handle(ClaimsPrincipal user)
        {
            if (user.Identity is null || !user.Identity.IsAuthenticated)
            {
                return Task.FromResult(Results.Unauthorized());
            }

            var identity = (ClaimsIdentity)user.Identity;
            var roles = identity.FindAll(identity.RoleClaimType).Select(c =>new
            {
                Issuer = c.Issuer,
                OriginalIssuer = c.OriginalIssuer,
                Type = c.Type,
                Value = c.Value,
                ValueType = c.ValueType
            });

            return Task.FromResult<IResult>(TypedResults.Json(roles));
        }
}