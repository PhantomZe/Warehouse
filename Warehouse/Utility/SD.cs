namespace Warehouse.Utility
{
    public class SD
    {
        public static string WarehouseAPIBase { get; set; }

		public const string RoleAdmin = "ADMIN";
		public const string RoleStaff = "Staff";
		public const string StatusActive = "Active";
		public const string StatusNonActive = "Non-Active";
		public enum ApiType
        {
            GET,
            POST, 
            PUT, 
            DELETE, 
            TRACE,
        }
    }
}
