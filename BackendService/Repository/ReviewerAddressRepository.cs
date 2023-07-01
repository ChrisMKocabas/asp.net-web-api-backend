using System;
using BackendService.Data;
using BackendService.Interfaces;
using BackendService.Models;

namespace BackendService.Repository
{
    public class ReviewerAddressRepository : IReviewerAddressRepository
    {
        private readonly DataContext _context;

        public ReviewerAddressRepository(DataContext context)
        {
            _context = context;

        }

        public bool AddressExists(int reviewerAddressId)
        {
            return _context.ReviewerAddresses.Any(r => r.Id == reviewerAddressId);
        }

        public bool CreateReviewerAddress(ReviewerAddress reviewerAddress)
        {
            _context.Add(reviewerAddress);
            return Save();
        }

        public bool DeleteReviewerAddress(ReviewerAddress reviewerAddress)
        {
            _context.Remove(reviewerAddress);
            return Save();
        }

        public bool DeleteReviewerAddresses(List<ReviewerAddress> reviewerAddresses)
        {
            _context.RemoveRange(reviewerAddresses);
            return Save();
        }

        public ReviewerAddress GetAddress(int reviewerAddressId)
        {
            return _context.ReviewerAddresses.Where(r => r.Id == reviewerAddressId).FirstOrDefault();
        }

        public ICollection<ReviewerAddress> GetAddresses()
        {
            return _context.ReviewerAddresses.ToList();
        }

        public ICollection<ReviewerAddress> GetAllAddressesOfaReviewer(int reviewerId)
        {
            return _context.ReviewerAddresses.Where(r => r.ReviewerId == reviewerId).ToList();
        }

        public ReviewerAddress GetDefaultAddressOfReviewer(int reviewerId)
        {
            return _context.ReviewerAddresses.Where(r => r.ReviewerId == reviewerId && r.DefaultAddress).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateReviewerAddress(ReviewerAddress reviewerAddress)
        {
            _context.Update(reviewerAddress);
            return Save();
        }
    }
}

