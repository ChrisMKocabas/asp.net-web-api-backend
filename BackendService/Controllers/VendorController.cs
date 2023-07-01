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
        private readonly ICountryRepository _countryRepository;

        public VendorController(IVendorRepository vendorRepository,IMapper mapper, ICountryRepository countryRepository)
		{
            _vendorRepository = vendorRepository;
            _mapper = mapper;
            _countryRepository = countryRepository;
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

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateVendor([FromBody] VendorDto vendorCreate, [FromQuery] string? CountryName)
        {
            if (vendorCreate == null)
                return BadRequest(ModelState);

            var vendors = _vendorRepository.GetVendors().Where(c => c.Name.Trim().ToUpper() == vendorCreate.Name.TrimEnd().ToUpper()).FirstOrDefault();

            if (vendors != null)
            {
                ModelState.AddModelError("", "Vendor already exists!");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vendorMap = _mapper.Map<Vendor>(vendorCreate);

            
            if (_countryRepository.CountryExists(vendorMap.CountryId))
            {
                
                vendorMap.Country = _countryRepository.GetCountry(vendorMap.CountryId);

            }
            else if (CountryName != null)
            {
                var country = _countryRepository.GetCountries().Where(c => c.Name.Trim().ToUpper() == CountryName.TrimEnd().ToUpper()).FirstOrDefault();

                if (country != null)
                {
                    vendorMap.Country = country;
                } else
                {
                    Country newCountry = new Country { Id = vendorMap.CountryId, Name = CountryName };

                    if (!_countryRepository.CreateCountry(newCountry))
                    {
                        ModelState.AddModelError("", "Something went wrong while saving.");
                    }

                    vendorMap.Country = newCountry;


                }
            }
                 
            if (!_vendorRepository.CreateVendor(vendorMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
            }

            return Ok("Successfully created!");
        }


        [HttpPut("{vendorId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCountry(int vendorId, [FromBody] VendorDto updatedVendor)
        {
            if (updatedVendor == null)
                return BadRequest(ModelState);

            if (vendorId != updatedVendor.Id)
                return BadRequest(ModelState);

            if (!_vendorRepository.VendorExists(vendorId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var vendorMap = _mapper.Map<Vendor>(updatedVendor);

            if (!_vendorRepository.UpdateVendor(vendorMap))
            {
                ModelState.TryAddModelError("", "Something went wrong updating vendor");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }


        [HttpDelete("{vendorId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCategory(int vendorId)
        {
            if (!_vendorRepository.VendorExists(vendorId))
            {
                return NotFound();
            }

            var vendorToDelete = _vendorRepository.GetVendor(vendorId);


            if (_vendorRepository.GetProductByVendor(vendorId).Any())
            {
                return BadRequest("Unable to delete. Vendor in use.");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_vendorRepository.DeleteVendor(vendorToDelete))
            { ModelState.AddModelError("", "Something went wrong deleting vendor"); }

            return NoContent();
        }
    }
}

