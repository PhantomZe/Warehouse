namespace Warehouse.Service.API.Models.Dto
{
	public class TransactionListItemDto
	{
		public int TransactionItemID { get; set; }
		public int ItemID { get; set; }
		public string ItemName { get; set; }
		public string RecipientName { get; set; }
		public int ItemAmount { get; set; }
		public int ItemPrice { get; set; }
		public int UserID { get; set; }
		public string UserName { get; set; }
		public DateTime TransactionDate { get; set; }

	}
}
