using System;
namespace BackendService.Models
{
	public class ProductVendor
	{
		public int ProductId { get; set; }
		public int VendorId { get; set; }
		public Product Product { get; set; }
		public Vendor Vendor { get; set; }

	}
}

