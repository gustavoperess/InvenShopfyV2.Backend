using System.Text.Json.Serialization;

namespace InvenShopfy.Core.Responses;

public class Response<TData>
{
    private readonly int _code;
    
    [JsonConstructor]
    public Response() // constructor without parameters 
    {
        _code = Configuration.DefaultStatusCode;
    }

    public Response(TData? data,  int code = Configuration.DefaultStatusCode, string? message = null)
    {
        Data = data;
        Message = message;
        _code = code;
    }
    public TData? Data { get; set; }
    public string? Message { get; set; }
    
    [JsonIgnore] // THIS HIDES IT FROM THE RESPONSE, WE DON'T NEED TO SEE THE BELOW CODE JUST THE MESSAGE
    public bool IsSuccess => _code is >= Configuration.DefaultStatusCode and <= 299;
}