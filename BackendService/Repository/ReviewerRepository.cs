using System;
using AutoMapper;
using BackendService.Data;
using BackendService.Interfaces;
using BackendService.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendService.Repository
{
	public class ReviewerRepository:IReviewerRepository
	{
        private readonly DataContext _context;

        public ReviewerRepository(DataContext context)
		{
            _context = context;
        }

        public Reviewer GetReviewer(int reviewerId)
        {
            return _context.Reviewers.Where(r => r.Id == reviewerId).Include(e => e.Reviews).FirstOrDefault();
        }

        public bool ReviewerExists(int reviewerId)
        {
            return _context.Reviewers.Any(r => r.Id == reviewerId);
        }

        public ICollection<Reviewer> GetReviewers()
        {
            return _context.Reviewers.ToList();
        }

        public ICollection<Review> GetReviewsByReviewer(int reviewerId)
        {
            return _context.Reviewers.Where(r => r.Id == reviewerId).SelectMany(r => r.Reviews).ToList();
        }
    }
}

