using System;
using BackendService.Data;
using BackendService.Interfaces;
using BackendService.Models;

namespace BackendService.Repository
{
	public class ProductRepository : IProductRepository
	{
        private readonly DataContext _context;

        public ProductRepository(DataContext context)
		{
			_context = context;
		}

        public Product GetProduct(int id)
        {
            return _context.Products.Where(p => p.Id == id).FirstOrDefault();
        }

        public Product GetProduct(string name)
        {
            return _context.Products.Where(p => p.Name == name).FirstOrDefault();
        }

        public decimal GetProductRating(int prodId)
        {
            var reviews = _context.Reviews.Where(p => p.Product.Id == prodId);

            if (reviews.Count() <= 0)
                return 0;

            return ((decimal)reviews.Sum(r => r.Rating / reviews.Count()));

        }

        public ICollection<Product> GetProducts()
		{
			return _context.Products.OrderBy(p => p.Id).ToList();
		}

        public bool ProductExists(int prodId)
        {
            return _context.Products.Any(p => p.Id == prodId);
        }
    }
}

