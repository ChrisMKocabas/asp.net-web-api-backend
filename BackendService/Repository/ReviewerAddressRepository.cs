using System;
using BackendService.Interfaces;
using BackendService.Models;

namespace BackendService.Repository
{
    public class ReviewerAddressRepository : IReviewerAddressRepository
    {
        public bool AddressExists(int id)
        {
            throw new NotImplementedException();
        }

        public ReviewerAddress GetAddress(int id)
        {
            throw new NotImplementedException();
        }

        public ReviewerAddress GetAddressByReviewer(int reviewerId)
        {
            throw new NotImplementedException();
        }

        public ICollection<ReviewerAddress> GetAddresses()
        {
            throw new NotImplementedException();
        }

        public ICollection<ReviewerAddress> GetAllAddressesOfaReviewer(int reviewerId)
        {
            throw new NotImplementedException();
        }

        public bool SetDefaultAddress(int id)
        {
            throw new NotImplementedException();
        }
    }
}

