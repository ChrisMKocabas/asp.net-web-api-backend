using System;
using BackendService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BackendService.Models;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using BackendService.Dto;
using BackendService.Repository;

namespace BackendService.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : Controller
	{
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IVendorRepository _vendorRepository;
        private readonly IReviewRepository _reviewRepository;

        public ProductController(IProductRepository productRepository, IMapper mapper, IVendorRepository vendorRepository,IReviewRepository reviewRepository)
		{
			_productRepository = productRepository;
            _mapper = mapper;
            _vendorRepository = vendorRepository;
            _reviewRepository = reviewRepository;
        }


        [HttpGet]
		[ProducesResponseType(200,Type = typeof(IEnumerable<Product>))]
		public IActionResult GetProducts()
		{
			var products = _mapper.Map<List<ProductDto>>(_productRepository.GetProducts());

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(products);

			
		}


		[HttpGet("{prodId}")]
		[ProducesResponseType(200, Type = typeof(Product))]
		[ProducesResponseType(400)]
		public IActionResult GetProduct(int prodId)
		{
			if (!_productRepository.ProductExists(prodId))
				return NotFound();

			var product = _mapper.Map<ProductDto>(_productRepository.GetProduct(prodId));

			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			return Ok(product);

		}

		[HttpGet("{prodId}/rating")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
		public IActionResult GetProductRating(int prodId)
		{
			if (!_productRepository.ProductExists(prodId))
				return NotFound();

			var rating = _productRepository.GetProductRating(prodId);

			if (!ModelState.IsValid)
				return BadRequest();

			return Ok(rating);
		}


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateProduct([FromQuery] int vendorId, [FromQuery] int catId, [FromBody] ProductDto productCreate)
        {
            if (productCreate == null)
                return BadRequest(ModelState);

            var products = _productRepository.GetProducts().Where(c => c.Name.Trim().ToUpper() == productCreate.Name.TrimEnd().ToUpper()).FirstOrDefault();

            if (products != null)
            {
                ModelState.AddModelError("", "Product already exists!");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productMap = _mapper.Map<Product>(productCreate);


            if (!_productRepository.CreateProduct(vendorId, catId,productMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created!");
        }


        [HttpPut("{productId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCountry(int productId, [FromBody] ProductDto updatedProduct, [FromQuery] int vendorId = -999, [FromQuery] int updatedVendorId = -999, [FromQuery] int categoryId = -999, [FromQuery] int updatedCategoryId = -999)
        {
            if (updatedProduct == null)
                return BadRequest(ModelState);

            if (productId != updatedProduct.Id)
                return BadRequest(ModelState);

            if (!_productRepository.ProductExists(productId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var productMap = _mapper.Map<Product>(updatedProduct);

            if (!_productRepository.UpdateProduct(productId,vendorId, updatedVendorId, categoryId, updatedCategoryId, productMap))
            {
                ModelState.TryAddModelError("", "Something went wrong updating product");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }


        [HttpDelete("{productId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCategory(int productId)
        {
            if (!_productRepository.ProductExists(productId))
            {
                return NotFound();
            }

            var ReviewsToDelete = _reviewRepository.GetReviewsOfaProduct(productId);

            var productToDelete = _productRepository.GetProduct(productId);

            if (!_reviewRepository.DeleteReviews(ReviewsToDelete.ToList())){
                ModelState.AddModelError("", "Something went wrong deleting reviews of product.");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_productRepository.DeleteProduct(productToDelete))
            { ModelState.AddModelError("", "Something went wrong deleting product"); }

            return NoContent();
        }
    }
}

