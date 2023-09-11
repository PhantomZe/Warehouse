using System.ComponentModel.DataAnnotations;

namespace Warehouse.Service.API.Models
{
    public class Item
    {
        [Key]
        public int ItemID { get; set; }
        [Required]
        public string ItemName { get; set; }
        public int ItemAmount { get; set; }
        public int Status { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
