using System;
using AutoMapper;
using CarLinePickup.API.Models.Response;
using CarLinePickup.Domain.Models;
using CarLinePickup.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarLinePickup.API.Models.Request.Update;
using System.ComponentModel.DataAnnotations;

namespace CarLinePickup.API.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{v:apiVersion}/addresses")]
    public class AddressController : Controller
    {
        private readonly IAddressService<Address> _addressService;
        private readonly IMapper _mapper;

        public AddressController(IAddressService<Address> addressService, IMapper mapper)
        {
            _addressService = addressService;
            _mapper = mapper;
        }

        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok();
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var address = await _addressService.GetAsync(id);

            if (address == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<AddressResponse>(address);

            return Ok(response);
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllAddresses([Required, FromQuery] int pageSize, [Required, FromQuery] int pageIndex, [Required, FromQuery] string orderBy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var addresses = await _addressService.GetAllAsync(pageSize: pageSize, pageIndex: pageIndex, orderBy: orderBy);

            if (addresses == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<PagedResult<Address>, PagedResponse<AddressResponse>>(addresses);

            return Ok(response);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            await _addressService.DeleteAsync(id);

            return Ok();
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] AddressRequestUpdate addressRequestUpdate)
        {
            if (addressRequestUpdate == null)
            {
                return BadRequest();
            }

            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var domainAddress = await _addressService.GetAsync(id);

            var updatedDomainAddress = _mapper.Map(addressRequestUpdate, domainAddress);

            await _addressService.UpdateAsync(updatedDomainAddress);

            return NoContent();
        }
    }
}