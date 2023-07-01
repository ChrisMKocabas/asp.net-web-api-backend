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
    public class ReviewerAddressController:Controller
	{
        private readonly IReviewerAddressRepository _reviewerAddressRepository;
        private readonly IMapper _mapper;
        private readonly IReviewerRepository _reviewerRepository;

        public ReviewerAddressController(IReviewerAddressRepository reviewerAddressRepository, IMapper mapper,IReviewerRepository reviewerRepository)
		{
            _reviewerAddressRepository = reviewerAddressRepository;
            _mapper = mapper;
            _reviewerRepository = reviewerRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewerAddress>))]
        public IActionResult GetAddresses()
        {
            var reviewerAddresses = _mapper.Map<List<ReviewerAddressDto>>(_reviewerAddressRepository.GetAddresses());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviewerAddresses);


        }

        [HttpGet("{reviewerAddressId}")]
        [ProducesResponseType(200, Type = typeof(ReviewerAddress))]
        [ProducesResponseType(400)]
        public IActionResult GetAddress(int reviewerAddressId)
        {
            if (!_reviewerAddressRepository.AddressExists(reviewerAddressId))
                return NotFound();

            var reviewerAddress = _mapper.Map<ReviewerAddressDto>(_reviewerAddressRepository.GetAddress(reviewerAddressId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviewerAddress);

        }

        [HttpGet("reviewers/{reviewerId}")]
        [ProducesResponseType(200, Type = typeof(ReviewerAddress))]
        [ProducesResponseType(400)]
        public IActionResult GetAllAddressesOfaReviewer(int reviewerId)
        {

            var address = _mapper.Map<List<ReviewerAddressDto>>(_reviewerAddressRepository.GetAllAddressesOfaReviewer(reviewerId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(address);
        }

        [HttpGet("default/reviewers/{reviewerId}")]
        [ProducesResponseType(200, Type = typeof(ReviewerAddress))]
        [ProducesResponseType(400)]
        public IActionResult GetDefaultAddressOfReviewer(int reviewerId)
        {

            var defaultAddress = _mapper.Map<ReviewerAddressDto>(_reviewerAddressRepository.GetDefaultAddressOfReviewer(reviewerId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(defaultAddress);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReviewerAddress([FromQuery] int reviewerId, [FromBody] ReviewerAddressDto reviewerAddressCreate)
        {
            if (reviewerAddressCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewerAddressMap = _mapper.Map<ReviewerAddress>(reviewerAddressCreate);

            var defaultAddress = new List<ReviewerAddress>();

            defaultAddress.Add(_reviewerAddressRepository.GetDefaultAddressOfReviewer(reviewerId));
            
            if(defaultAddress.Count == 0 || reviewerAddressMap.DefaultAddress)
            {
                defaultAddress.ForEach(x => x.DefaultAddress = false);
                reviewerAddressMap.DefaultAddress = true;
            }

            if (!_reviewerAddressRepository.CreateReviewerAddress(reviewerAddressMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving new address.");
                return StatusCode(500, ModelState);
            }

            if (reviewerAddressMap.DefaultAddress)
            {
                var reviewer = _reviewerRepository.GetReviewer(reviewerId);
                reviewer.DefaultAddressId = _reviewerAddressRepository.GetAllAddressesOfaReviewer(reviewerId).First(r => r.DefaultAddress).Id;

                if (!_reviewerRepository.UpdateReviewer(reviewer))
                {
                    ModelState.TryAddModelError("", "Something went wrong updating default address");
                    return StatusCode(500, ModelState);
                }
            }

            return Ok("Successfully created!");
        }


        [HttpPut("{reviewerAddressId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReviewerAddress([FromQuery] int reviewerAddressId, [FromBody] ReviewerAddressDto updatedReviewerAddress)
        {
            if (updatedReviewerAddress == null)
                return BadRequest(ModelState);

            if (reviewerAddressId != updatedReviewerAddress.Id)
                return BadRequest(ModelState);

            if (!_reviewerAddressRepository.AddressExists(reviewerAddressId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewerAddressMap = _mapper.Map<ReviewerAddress>(updatedReviewerAddress);


            if (updatedReviewerAddress.DefaultAddress)
            {

                var oldDefault = _reviewerAddressRepository.GetDefaultAddressOfReviewer(updatedReviewerAddress.ReviewerId);

                  oldDefault.DefaultAddress = false;

                if (!_reviewerAddressRepository.UpdateReviewerAddress(oldDefault))
                {
                    ModelState.AddModelError("", "Something went wrong while revoking old default address.");
                    return StatusCode(500, ModelState);
                }

                var reviewer = _reviewerRepository.GetReviewer(updatedReviewerAddress.ReviewerId);

                reviewer.DefaultAddressId = updatedReviewerAddress.Id;

                if (!_reviewerRepository.UpdateReviewer(reviewer))
                {
                    ModelState.TryAddModelError("", "Something went wrong updating default address");
                    return StatusCode(500, ModelState);
                }
            }

            if (!_reviewerAddressRepository.UpdateReviewerAddress(reviewerAddressMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating the address.");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created!");
        }

        [HttpDelete("{reviewerAddressId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCategory(int reviewerAddressId)
        {
            if (!_reviewerAddressRepository.AddressExists(reviewerAddressId))
            {
                return NotFound();
            }

            var reviewerAddressToDelete = _reviewerAddressRepository.GetAddress(reviewerAddressId);


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_reviewerAddressRepository.DeleteReviewerAddress(reviewerAddressToDelete))
            { ModelState.AddModelError("", "Something went wrong deleting review"); }

            if(reviewerAddressToDelete.DefaultAddress)
            {

                var firstReviewerAddress = _reviewerAddressRepository.GetAllAddressesOfaReviewer(reviewerAddressToDelete.ReviewerId).FirstOrDefault();
                firstReviewerAddress.DefaultAddress = true;

                if (!_reviewerAddressRepository.UpdateReviewerAddress(firstReviewerAddress))
                {
                    ModelState.AddModelError("", "Something went wrong while setting new defaul address.");
                    return StatusCode(500, ModelState);
                }

                var reviewer = _reviewerRepository.GetReviewer(reviewerAddressToDelete.ReviewerId);

                reviewer.DefaultAddressId = firstReviewerAddress.Id;

                if (!_reviewerRepository.UpdateReviewer(reviewer))
                {
                    ModelState.TryAddModelError("", "Something went wrong updating default address id for reviewe");
                    return StatusCode(500, ModelState);
                }
            }

            return NoContent();
        }

        [HttpDelete("deleteall/{reviewerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReviewerAddresses(int reviewerId)
        {
            if (!_reviewerRepository.ReviewerExists(reviewerId))
            {
                return NotFound();
            }

            var reviewerAddressesToDelete = new List<ReviewerAddress>(_reviewerAddressRepository.GetAllAddressesOfaReviewer(reviewerId));

            if (reviewerAddressesToDelete.Count == 0)
            {
                return BadRequest("No address to delete!");
            }
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_reviewerAddressRepository.DeleteReviewerAddresses(reviewerAddressesToDelete))
            { ModelState.AddModelError("", "Something went wrong clearing all addresses of the user."); }

            var reviewer = _reviewerRepository.GetReviewer(reviewerId);

            reviewer.DefaultAddressId = null;

            if (!_reviewerRepository.UpdateReviewer(reviewer))
            {
                ModelState.TryAddModelError("", "Something went wrong clearing default address for reviewer!");
                return StatusCode(500, ModelState);
            }
            

            return NoContent();
        }


    }
}

