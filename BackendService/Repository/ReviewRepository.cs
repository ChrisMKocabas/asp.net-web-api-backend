using System;
using AutoMapper;
using BackendService.Data;
using BackendService.Interfaces;
using BackendService.Models;

namespace BackendService.Repository
{
	public class ReviewRepository:IReviewRepository
	{
        private readonly DataContext _context;
   

        public ReviewRepository(DataContext context)
		{
            _context = context;
      
        }

        public Review GetReview(int reviewId)
        {
            return _context.Reviews.Where(r => r.Id == reviewId).FirstOrDefault();
        }

        public ICollection<Review> GetReviews()
        {
            return _context.Reviews.ToList();
        }

        public ICollection<Review> GetReviewsOfaProduct(int prodId)
        {
            return _context.Reviews.Where(r => r.Product.Id == prodId).ToList();
        }

        public bool ReviewExists(int reviewId)
        {
            return _context.Reviews.Any(r => r.Id == reviewId);
        }
    }
}

