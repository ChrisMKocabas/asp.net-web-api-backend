using System;
using BackendService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BackendService.Models;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using BackendService.Dto;

namespace BackendService.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : Controller
	{
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductController(IProductRepository productRepository, IMapper mapper)
		{
			_productRepository = productRepository;
            _mapper = mapper;
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
    }
}

