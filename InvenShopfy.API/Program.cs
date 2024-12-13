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

Console.WriteLine($"Environment: {builder.Environment.EnvironmentName}");
Console.WriteLine($"Backend URL: {builder.Configuration["BackendUrl"]}");
Console.WriteLine($"Frontend URL: {builder.Configuration["FrontendUrl"]}");

var app = builder.Build();
if (app.Environment.IsDevelopment())
    app.ConfigureDevEnvironment();

app.UseCors(Configuration.CorsPolicyName);
app.UseSecurity();
app.MapEndpoints();
app.UseSerilogRequestLogging();

app.Run();


//InvenShopfy123!