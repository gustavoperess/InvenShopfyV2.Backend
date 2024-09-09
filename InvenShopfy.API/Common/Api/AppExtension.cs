namespace InvenShopfy.API.Common.Api;

public static class AppExtension
{
    public static void ConfigureDevEnvironment(this WebApplication app)
    {
        // app.UseMiddleware<ErrorHandlingMiddleware>();
        app.UseSwagger();
        app.UseSwaggerUI();
        // app.MapSwagger().RequireAuthorization();
    }
    
    public static void UseSecurity(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }
}