﻿using System;
using BackendService.Data;
using BackendService.Interfaces;
using BackendService.Models;

namespace BackendService.Repository
{
	public class VendorRepository:IVendorRepository
	{
        private readonly DataContext _context;

        public VendorRepository(DataContext context) { 
		
            _context = context;
        }

        public ICollection<Product> GetProductByVendor(int vendorId)
        {
            return _context.ProductVendors.Where(vp => vp.Vendor.Id == vendorId).Select(vp => vp.Product).OrderBy(p=>p.Id).ToList();
        }

        public Vendor GetVendor(int vendorId)
        {
            return _context.Vendors.Where(v => v.Id == vendorId).FirstOrDefault();
        }

        public ICollection<Vendor> GetVendorOfAProduct(int prodId)
        {
            return _context.ProductVendors.Where(vp => vp.Product.Id == prodId).Select(vp => vp.Vendor).OrderBy(v=>v.Id).ToList();
        }

        public ICollection<Vendor> GetVendors()
        {
            return _context.Vendors.OrderBy(v=>v.Id).ToList();
        }

        public bool VendorExists(int vendorId)
        {
            return _context.Vendors.Any(v => v.Id == vendorId);
        }
    }
}
