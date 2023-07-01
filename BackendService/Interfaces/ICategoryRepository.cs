using System;
using BackendService.Models;

namespace BackendService.Interfaces
{
	public interface ICategoryRepository
	{
		ICollection<Category> GetCategories();

		Category GetCategory(int id);

		ICollection<Product> GetProductsByCategory(int categoryId);

		bool CategoryExists(int id);

		bool CreateCategory(Category category);

		bool UpdateCategory(Category category);

		bool DeleteCategory(Category category);

		bool Save();

	}
}

