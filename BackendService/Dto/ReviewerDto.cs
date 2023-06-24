using System;
using BackendService.Models;

namespace BackendService.Dto
{
	public class ReviewerDto
	{
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<ReviewDto>? Reviews { get; set; }
        public int? DefaultAddressId { get; set; }
        public ICollection<ReviewerAddressDto>? ReviewerAddresses { get; set; }
    }
}

