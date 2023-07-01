using System;
using BackendService.Models;

namespace BackendService.Interfaces
{
	public interface IReviewerAddressRepository
	{

		ICollection<ReviewerAddress> GetAddresses();

		ReviewerAddress GetAddress(int reviewerAddressId);

		ReviewerAddress GetDefaultAddressOfReviewer(int reviewerId);

        ICollection<ReviewerAddress> GetAllAddressesOfaReviewer(int reviewerId);

		bool AddressExists(int reviewerAddressId);

        bool CreateReviewerAddress(ReviewerAddress reviewerAddress);

        bool UpdateReviewerAddress(ReviewerAddress reviewerAddress);

        bool DeleteReviewerAddress(ReviewerAddress reviewerAddress);

        bool DeleteReviewerAddresses(List<ReviewerAddress> reviewerAddresses);

        bool Save();
    }
}

