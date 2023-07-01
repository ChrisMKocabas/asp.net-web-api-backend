using System;
using BackendService.Models;

namespace BackendService.Interfaces
{
	public interface IProductRepository
	{
		ICollection<Product> GetProducts();

		Product GetProduct(int id);
		Product GetProduct(string name);
		decimal GetProductRating(int prodId);

		bool ProductExists(int prodId);

		bool CreateProduct(int vendorId, int categoryId, Product product);

		bool UpdateProduct(int productId, int vendorId, int updatedVendorId, int categoryId, int updatedCategoryId, Product product);

		bool DeleteProduct(Product product);

		bool Save();
	}
}

