using System.ComponentModel.DataAnnotations;
using InvenShopfy.Core.Enum;

namespace InvenShopfy.Core.Requests.Transfers
{
    public class CreateTransferRequest : Request
    {
        [Required(ErrorMessage = "Please enter the Date the transfer was requested")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateOnly TransferDate { get; set; }
        

        [Required(ErrorMessage = "Invalid Warehouse Name")]
        public long FromWarehouseId { get; set; } 

        [Required(ErrorMessage = "Invalid Warehouse Name")]
        public long ToWarehouseId{ get; set; } 
        

        [Required(ErrorMessage = "Invalid TotalAmount")]
        [Range(1, (double)decimal.MaxValue, ErrorMessage = "Total Amount must be between {1} and {2}")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Invalid Reason")]
        [MaxLength(180, ErrorMessage = "Max len of 180 characters")]
        public string Reason { get; set; } = null!;
        
        
        [Required(ErrorMessage = "Invalid Product Id")]
        public long ProductId { get; set; }

        [Required(ErrorMessage = "Please select one of the Remark statuses")]
        [AllowedValues("Completed", "Pending", "InTransit",
            ErrorMessage = "Please select one of the allowed values: Completed, Pending, InTransit")]
        public string TransferStatus { get; set; } = ERemarkStatus.Duplicated.ToString();

        [MaxLength(500, ErrorMessage = "Max len of 500 characters")]
        public string TransferNote { get; set; } = null!;

        [Required(ErrorMessage = "Authorized by is required")]
        [MaxLength(80, ErrorMessage = "Max len of 80 characters")]
        public string AuthorizedBy { get; set; } = null!;
        
    }
}
