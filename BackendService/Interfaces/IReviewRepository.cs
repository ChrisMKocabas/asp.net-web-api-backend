using System;
using BackendService.Models;

namespace BackendService.Interfaces
{
	public interface IReviewRepository
	{
		ICollection<Review> GetReviews();

		Review GetReview(int reviewId);

		ICollection<Review> GetReviewsOfaProduct(int prodId);

		bool ReviewExists(int reviewId);

		bool CreateReview(Review review);

		bool UpdateReview(Review review);

		bool DeleteReview(Review review);

        bool DeleteReviews(List<Review> reviews);

        bool Save();
	}
}

