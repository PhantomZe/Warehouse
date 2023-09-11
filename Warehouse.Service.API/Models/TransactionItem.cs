using System.ComponentModel.DataAnnotations;

namespace Warehouse.Service.API.Models
{
    public class TransactionItem
    {
        [Key]
        public int TransactionItemID { get; set; }
        public int ItemID { get; set; }
        public string RecipientName { get; set; }
        public int ItemAmount { get; set; }
        public int ItemPrice { get; set; }
        public int UserID { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
