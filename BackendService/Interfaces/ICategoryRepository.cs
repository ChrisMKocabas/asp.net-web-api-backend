using System;
using BackendService.Models;

namespace BackendService.Interfaces
{
	public interface ICategoryRepository
	{
		ICollection<Category> GetCategories();

		Category GetCategory(int id);

		ICollection<Product> GetProductByCategory(int categoryId);

		bool CategoryExists(int id);

	}
}

