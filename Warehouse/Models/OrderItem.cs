namespace Warehouse.Models
{
	public class OrderItem
	{
		public int ItemID { get; set; }
        public string ItemName { get; set; }
        public int RemainingItemAmount { get; set; }
        public string RecipientName { get; set; }
		public int ItemAmount { get; set; }
		public int ItemPrice { get; set; }
		public int UserID { get; set; }
	}
}
