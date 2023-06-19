using System;
using BackendService.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendService.Data
{
	public class DataContext: DbContext
    {
		public DataContext(DbContextOptions<DataContext> options) : base(options)
		{

		}

		public DbSet<Category> Categories { get; set; }
		public DbSet<Country> Countries { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<ProductCategory> ProductCategories { get; set; }
		public DbSet<ProductVendor> ProductVendors{ get; set; }
        public DbSet<Review> Reviews { get; set; }
		public DbSet<Reviewer> Reviewers { get; set; }
		public DbSet<ReviewerAddress> ReviewerAddresses { get; set; }
		public DbSet<Vendor> Vendors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           

			modelBuilder.Entity<ProductCategory>()
				.HasKey(pc => new { pc.ProductId, pc.CategoryId });

			modelBuilder.Entity<ProductCategory>()
				.HasOne(p => p.Product)
				.WithMany(pc => pc.ProductCategories)
				.HasForeignKey(p => p.ProductId);

            modelBuilder.Entity<ProductCategory>()
            .HasOne(c => c.Category)
            .WithMany(pc => pc.ProductCategories)
            .HasForeignKey(c => c.CategoryId);


            modelBuilder.Entity<ProductVendor>()
            .HasKey(pv => new { pv.ProductId, pv.VendorId });

            modelBuilder.Entity<ProductVendor>()
                .HasOne(p => p.Product)
                .WithMany(pv => pv.ProductVendors)
                .HasForeignKey(p => p.ProductId);

            modelBuilder.Entity<ProductVendor>()
            .HasOne(v => v.Vendor)
            .WithMany(pv => pv.ProductVendors)
            .HasForeignKey(v => v.VendorId);

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Reviewer>()
            .HasOne(a => a.ReviewerAddress)
            .WithOne(a => a.Reviewer)
            .HasForeignKey<ReviewerAddress>(c => c.ReviewerID);
        }

    }
}

