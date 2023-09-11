namespace Warehouse.Service.API.Models.Dto
{
    public class ItemDto
    {
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public int ItemAmount { get; set; }
        public int Status { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
