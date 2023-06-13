using System;
namespace BackendService.Models
{
	public class Product
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int Price { get; set; }
		public ICollection<Review> Reviews { get; set; }

		public ICollection<ProductVendor> ProductVendors { get; set; }

		public ICollection<ProductCategory> ProductCategories { get; set; }



	}
}

