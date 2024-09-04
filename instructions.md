
# Normaly we create 
- Models
- Mapping
- Migrations

Core - Class Lib (client and server)
PWA - Balzor WASM (client)
API - API (runs in the server)

Instructions on how to organize the project. 

1 - Create a solution to group the projects
    dotnet new ln
2 - Create a core for your project, a classlib project that will be handling the middle of your project
    dotnet new classlib -o InvenShopfy.Core
3 - Add the core to your solution
    dotnet sln add ./InvenShopfy.Core
4 - Create a backend for the project
    dotnet new web -o InvenShopfy.API
3 - Add the API to your solution
    dotnet sln add ./InvenShopfy.API

