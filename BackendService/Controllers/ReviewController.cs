using System;
using AutoMapper;
using BackendService.Dto;
using BackendService.Interfaces;
using BackendService.Models;
using BackendService.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BackendService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController:Controller
	{
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public ReviewController(IReviewRepository reviewRepository, IMapper mapper)
		{
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        public IActionResult GetReviews()
        {
            var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviews());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviews);


        }

        [HttpGet("{reviewId}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        public IActionResult GetReview(int reviewId)
        {
            if (!_reviewRepository.ReviewExists(reviewId))
                return NotFound();

            var review = _mapper.Map<ReviewDto>(_reviewRepository.GetReview(reviewId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(review);

        }


        [HttpGet("reviews/{prodId}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewsOfaProduct(int prodId)
        {

            var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviewsOfaProduct(prodId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(reviews);
        }
    }
}

