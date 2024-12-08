using InvenShopfy.API.Common.Api;
using InvenShopfy.API.EndPoints;
using InvenShopfy.Core;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfiguration();
builder.AddSecurity();
builder.AddDataContexts();
builder.AddCrossOrigin();
builder.AddDocumentation();
builder.AddServices();
builder.CloudinaryConfiguration();
builder.AddSerilog();

var frontendUrl = builder.Configuration["FrontendUrl"];
var backendUrl = builder.Configuration["BackendUrl"];
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

Console.WriteLine($"FrontendUrl: {frontendUrl}");
Console.WriteLine($"BackendUrl: {backendUrl}");
Console.WriteLine($"ConnectionString: {connectionString}");

var app = builder.Build();
if (app.Environment.IsDevelopment())
    app.ConfigureDevEnvironment();

app.UseCors(Configuration.CorsPolicyName);
app.UseSecurity();
app.MapEndpoints();
app.UseSerilogRequestLogging();

app.Run();


// {
//     "email": "gustavo@gmail.com",
//     "password": "Gustavo123!"
// }


// {
//     "email": "ashley1234@gmail.com",
//     "password": "Gustavo123!"
// }