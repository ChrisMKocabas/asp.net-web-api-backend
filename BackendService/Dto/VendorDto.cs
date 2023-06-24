using System;
using BackendService.Models;

namespace BackendService.Dto
{
	public class VendorDto
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime JoinDate { get; set; }
        public int? CountryId { get; set; }
    }
}

