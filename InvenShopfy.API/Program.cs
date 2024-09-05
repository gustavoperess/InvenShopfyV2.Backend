using InvenShopfy.API.Common.Api;
using InvenShopfy.API.EndPoints;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfiguration();
builder.AddSecurity();
builder.AddDataContexts();
// builder.AddCrossOrigin();
builder.AddDocumentation();
builder.AddServices();



var app = builder.Build();
if (app.Environment.IsDevelopment())
    app.ConfigureDevEnvironment();

// app.UseCors(Configuration.CorsPolicyName);
app.UseSecurity();
app.MapEndpoints();

app.Run();
