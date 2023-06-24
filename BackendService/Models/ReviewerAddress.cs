using System;
namespace BackendService.Models
{
	public class ReviewerAddress
    {
		public int Id { get; set; }
		public string StreetNumber { get; set; }
		public string Street { get; set; }
		public string PostalCode { get; set; }
		public string City { get; set; }
		public string? StateProvince { get; set; }
		public string Country { get; set; }
        public int ReviewerId { get; set; }
		public bool DefaultAddress { get; set; }
		public Reviewer Reviewer { get; set; }

	}	
}

