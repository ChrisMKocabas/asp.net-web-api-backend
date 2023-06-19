using System;
using BackendService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BackendService.Models;
using System.ComponentModel.DataAnnotations;

namespace BackendService.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : Controller
	{
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}


        [HttpGet]
		[ProducesResponseType(200,Type = typeof(IEnumerable<Product>))]
		public IActionResult GetProducts()
		{
			var products = _productRepository.GetProducts();

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(products);

			
		}
	}
}

