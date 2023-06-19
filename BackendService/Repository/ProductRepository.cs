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

		public ICollection<Product> GetProducts()
		{
			return _context.Products.OrderBy(p => p.Id).ToList();
		}
	}
}

