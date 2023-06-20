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
    public class VendorController:Controller
	{
        private readonly IVendorRepository _vendorRepository;
        private readonly IMapper _mapper;

        public VendorController(IVendorRepository vendorRepository,IMapper mapper)
		{
            _vendorRepository = vendorRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Vendor>))]
        public IActionResult GetVendors()
        {
            var vendors = _mapper.Map<List<VendorDto>>(_vendorRepository.GetVendors());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(vendors);


        }


        [HttpGet("{vendorId}")]
        [ProducesResponseType(200, Type = typeof(Vendor))]
        [ProducesResponseType(400)]
        public IActionResult GetVendor(int vendorId)
        {
            if (!_vendorRepository.VendorExists(vendorId))
                return NotFound();

            var vendor = _mapper.Map<VendorDto>(_vendorRepository.GetVendor(vendorId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(vendor);

        }

        [HttpGet("{vendorId}/product")]
        [ProducesResponseType(200, Type = typeof(ICollection<Product>))]
        [ProducesResponseType(400)]
        public IActionResult GetProductByVendor(int vendorId)
        {
            if (!_vendorRepository.VendorExists(vendorId))
                return NotFound();

            var product = _mapper.Map<List<ProductDto>>(_vendorRepository.GetProductByVendor(vendorId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(product);
        }
    }
}

