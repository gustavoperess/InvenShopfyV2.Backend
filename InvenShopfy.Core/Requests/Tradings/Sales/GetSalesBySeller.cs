using System.ComponentModel.DataAnnotations;

namespace InvenShopfy.Core.Requests.Tradings.Sales;

public class GetSalesBySeller : Request
{
    [Required(ErrorMessage = "Invalid Biller Id")]
    public long BillerId { get; set; }

}