using System;
using BackendService.Models;

namespace BackendService.Interfaces
{
	public interface IReviewerRepository
	{
		ICollection<Reviewer> GetReviewers();

		Reviewer GetReviewer(int reviewerId);

		ICollection<Review> GetReviewsByReviewer(int reviewerId);

		bool ReviewerExists(int reviewerId);

		ReviewerAddress GetDefaultAddress(int reviewerId);

		ICollection<ReviewerAddress> GetAllAddresses(int id);

	}
}

