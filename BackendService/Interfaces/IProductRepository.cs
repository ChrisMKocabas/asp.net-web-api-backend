using System;
using BackendService.Models;

namespace BackendService.Interfaces
{
	public interface IProductRepository
	{
		ICollection<Product> GetProducts();
	}
}

