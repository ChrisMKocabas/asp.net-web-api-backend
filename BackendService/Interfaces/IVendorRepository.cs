using System;
using BackendService.Models;

namespace BackendService.Interfaces
{
	public interface IVendorRepository
	{
		ICollection<Vendor>GetVendors();

		Vendor GetVendor(int vendorId);

		ICollection<Vendor> GetVendorOfAProduct(int prodId);

		ICollection<Product> GetProductByVendor(int vendorId);

		bool VendorExists(int vendorId);

	}
}

