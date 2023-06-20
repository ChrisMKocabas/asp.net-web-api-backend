using System;
using AutoMapper;
using BackendService.Dto;
using BackendService.Models;

namespace BackendService.Helper
{
	public class MappingProfiles:Profile
	{
		public MappingProfiles()
		{
			CreateMap<Product, ProductDto>();
			CreateMap<Category, CategoryDto>();
			CreateMap<Country, CountryDto>();
			CreateMap<Vendor, VendorDto>();
			CreateMap<Review, ReviewDto>();
			CreateMap<Reviewer, ReviewerDto>();
		}
	}
}

