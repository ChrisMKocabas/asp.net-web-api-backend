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
	}
}

