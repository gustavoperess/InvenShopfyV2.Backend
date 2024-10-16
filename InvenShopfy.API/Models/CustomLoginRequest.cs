using Microsoft.AspNetCore.Identity;

namespace InvenShopfy.API.Models;

public class CustomLoginRequest 
{
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
  
}