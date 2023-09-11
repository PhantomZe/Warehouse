namespace Warehouse.Models
{
    public class ItemDto
    {
        public int ItemID { get; set; } = 0;
        public string ItemName { get; set; } = string.Empty;
        public int ItemAmount { get; set; }
        public int Status { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}
