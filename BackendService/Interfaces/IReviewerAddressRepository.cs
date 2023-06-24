using System;
using BackendService.Models;

namespace BackendService.Interfaces
{
	public interface IReviewerAddressRepository
	{

		ICollection<ReviewerAddress> GetAddresses();

		ReviewerAddress GetAddress(int id);

		ReviewerAddress GetAddressByReviewer(int reviewerId);

        ICollection<ReviewerAddress> GetAllAddressesOfaReviewer(int reviewerId);

		bool AddressExists(int id);

		bool SetDefaultAddress(int id);
    }
}

