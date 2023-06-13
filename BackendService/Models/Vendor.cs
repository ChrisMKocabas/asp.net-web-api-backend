using System;
namespace BackendService.Models
{
	public class Vendor
	{
		
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime JoinDate { get; set; }
		public Country Country { get; set; }

        public ICollection<ProductVendor> ProductVendors { get; set; }
    }
}

