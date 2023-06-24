using System;
namespace BackendService.Models
{
	public class Reviewer
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public ICollection<Review>? Reviews { get; set; }
		public int? DefaultAddressId { get;set; }
		public ICollection<ReviewerAddress>? ReviewerAddresses { get; set; }

	}
}

