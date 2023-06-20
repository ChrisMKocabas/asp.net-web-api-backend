using System;
using BackendService.Models;

namespace BackendService.Interfaces
{
	public interface ICountryRepository
	{
		ICollection<Country> GetCountries();

		Country GetCountry(int id);

		Country GetCountryByVendor(int vendorId);

		ICollection<Vendor> GetVendorsFromCountry(int countryId);

		bool CountryExists(int id);
	}
}

