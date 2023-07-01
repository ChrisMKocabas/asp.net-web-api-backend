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

        bool CreateCountry(Country country);

        bool UpdateCountry(Country country);

        bool DeleteCountry(Country country);

        bool Save();
   
    }
}

