using System;
using BackendService.Data;
using BackendService.Interfaces;
using BackendService.Models;

namespace BackendService.Repository
{
	public class CategoryRepository:ICategoryRepository
	{
        private DataContext _context;

        public CategoryRepository(DataContext context)
		{
            _context = context;
		}

        public bool CreateCategory(Category category)
        {
            _context.Add(category);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        bool ICategoryRepository.CategoryExists(int id)
        {
            return _context.Categories.Any(c => c.Id == id);
        }

        ICollection<Category> ICategoryRepository.GetCategories()
        {
            return _context.Categories.OrderBy(c=>c.Name).ToList();
        }

        Category ICategoryRepository.GetCategory(int id)
        {
            return _context.Categories.Where(e => e.Id == id).FirstOrDefault();
        }

        ICollection<Product> ICategoryRepository.GetProductByCategory(int categoryId)
        {
            return _context.ProductCategories.Where(e => e.CategoryId == categoryId).Select(c => c.Product).ToList();
        }
    }
}

