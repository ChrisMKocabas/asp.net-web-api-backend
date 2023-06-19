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
		}
	}
}

