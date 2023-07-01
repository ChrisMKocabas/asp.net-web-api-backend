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

        public bool  CreateProduct(int vendorId, int categoryId, Product product)
        {
            var productVendorEntity = _context.Vendors.Where(a => a.Id == vendorId).FirstOrDefault();
            var productCategoryEntity = _context.Categories.Where(c => c.Id == categoryId).FirstOrDefault();

            var productVendor = new ProductVendor()
            {
                Vendor = productVendorEntity,
                Product = product,
            };

            _context.Add(productVendor);

            var productCategory = new ProductCategory()
            {
                Category = productCategoryEntity,
                Product = product,

            };

            _context.Add(productCategory);

            _context.Add(product);

            return Save();
          
        }

        public bool DeleteProduct(Product product)
        {
            _context.Remove(product);

            return Save();
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

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateProduct(int productId, int vendorId, int updatedVendorId, int categoryId, int updatedCategoryId, Product product)
        {
            if (vendorId != -999 && _context.Vendors.Any(v => v.Id == vendorId) && _context.Vendors.Any(v => v.Id == updatedVendorId))
            {
                var vendorEntity = _context.Vendors.Where(a => a.Id == updatedVendorId).FirstOrDefault();

                var productVendorEntity = _context.ProductVendors.Where(pv => pv.VendorId == vendorId && pv.ProductId == productId).FirstOrDefault();

                // Remove the existing relationship
                _context.ProductVendors.Remove(productVendorEntity);
                _context.SaveChanges();

                // Create a new relationship
                var newProductVendorEntity = new ProductVendor
                {
                    Product = product,
                    Vendor = vendorEntity
                };

                _context.ProductVendors.Add(newProductVendorEntity);
            }



            if (categoryId != -999 && _context.Categories.Any(v => v.Id == categoryId) && _context.Categories.Any(v => v.Id == updatedCategoryId))
            {
                var categoryEntity = _context.Categories.Where(a => a.Id == updatedCategoryId).FirstOrDefault();

                var productCategoryEntity = _context.ProductCategories.Where(pc => pc.CategoryId == categoryId && pc.ProductId == productId).FirstOrDefault();

                // Remove the existing relationship
                _context.ProductCategories.Remove(productCategoryEntity);
                _context.SaveChanges();

                // Create a new relationship
                var newProductCategoryEntity = new ProductCategory
                {
                    Product = product,
                    Category = categoryEntity
                };

                _context.ProductCategories.Add(newProductCategoryEntity);
            }

            _context.Update(product);
            return Save();
        }
    }
}

