using System;
using AutoMapper;
using CarLinePickup.API.Models.Response;
using CarLinePickup.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarLinePickup.API.Models.Request.Create;
using FluentValidation;
using CarLinePickup.Domain.Models;
using CarLinePickup.API.Models.Request.Update;
using System.ComponentModel.DataAnnotations;
using Azure;

namespace CarLinePickup.API.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{v:apiVersion}/families")]
    public class FamilyController : Controller
    {
        private readonly IFamilyService<Family> _familyService;
        private readonly IFamilyMemberService<FamilyMember> _familyMemberService;
        private readonly IVehicleService<Vehicle> _vehicleService;
        private readonly IAddressService<Address> _addressService;
        private readonly IValidatorFactory _validatorFactory;
        private readonly IMapper _mapper;

        public FamilyController(IFamilyService<Family> familyService, IFamilyMemberService<FamilyMember> familyMemberService, IVehicleService<Vehicle> vehicleService, IAddressService<Address> addressService, IMapper mapper, IValidatorFactory validatorFactory)
        {
            _familyService = familyService;
            _vehicleService = vehicleService;
            _addressService = addressService;
            _familyMemberService = familyMemberService;
            _mapper = mapper;
            _validatorFactory = validatorFactory;
        }

        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok();
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var domainFamily = await _familyService.GetAsync(id);

            if (domainFamily == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<FamilyResponse>(domainFamily);

            return Ok(response);
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllFamilies([Required, FromQuery] int pageSize, [Required, FromQuery] int pageIndex, [Required, FromQuery] string orderBy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var families = await _familyService.GetAllAsync(pageSize: pageSize, pageIndex: pageIndex, orderBy: orderBy);

            if (families == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<PagedResult<Family>, PagedResponse<FamilyResponse>>(families);

            return Ok(response);
        }

        [HttpGet("{familyId:Guid}/familymembers")]
        public async Task<IActionResult> GetFamilyMembersByFamilyId(Guid familyId, [Required, FromQuery] int pageSize, [Required, FromQuery] int pageIndex, [Required, FromQuery] string orderBy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var familyMembers = await _familyMemberService.GetFamilyMembersByFamilyIdAsync(familyId, pageSize: pageSize, pageIndex: pageIndex, orderBy: orderBy);

            if (familyMembers == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<PagedResult<FamilyMember>, PagedResponse<FamilyMemberResponse>>(familyMembers);

            return Ok(response);
        }

        [HttpGet("{familyId:Guid}/familymembers/{familyMemberType}")]
        public async Task<IActionResult> GetFamilyMembersByType(Guid familyId, string familyMemberType, [Required, FromQuery] int pageSize, [Required, FromQuery] int pageIndex, [Required, FromQuery] string orderBy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var familyMembers = await _familyMemberService.GetFamilyMembersByFamilyIdAndTypeAsync(familyId, familyMemberType, pageSize: pageSize, pageIndex: pageIndex, orderBy: orderBy);

            if (familyMembers == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<PagedResult<FamilyMember>, PagedResponse<FamilyMemberResponse>>(familyMembers);

            return Ok(response);
        }

        [HttpGet("{familyId:Guid}/familymembers/{familyMemberTypeId:Guid}")]
        public async Task<IActionResult> GetFamilyMembersByTypeId(Guid familyId, Guid familyMemberTypeId, [Required, FromQuery] int pageSize, [Required, FromQuery] int pageIndex, [Required, FromQuery] string orderBy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var familyMembers = await _familyMemberService.GetFamilyMembersByFamilyIdAndTypeIdAsync(familyId, familyMemberTypeId, pageSize: pageSize, pageIndex: pageIndex, orderBy: orderBy);

            if (familyMembers == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<PagedResult<FamilyMember>, PagedResponse<FamilyMemberResponse>>(familyMembers);

            return Ok(response);
        }

        [HttpPost("{familyId:Guid}/vehicles")]
        public async Task<IActionResult> CreateVehicle(Guid familyId, [FromBody] VehicleRequestCreate vehicleRequestCreate)
        {
            if (vehicleRequestCreate == null)
            {
                return BadRequest();
            }

            if (familyId == Guid.Empty)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var domainVehicle = _mapper.Map<Domain.Models.Vehicle>(vehicleRequestCreate);

            var createdVehicle = await _vehicleService.CreateAsync(familyId, domainVehicle);

            var response = _mapper.Map<VehicleResponse>(createdVehicle);

            return CreatedAtAction(nameof(CreateVehicle), new { id = response.Id }, response);
        }

        [HttpPost()]
        public async Task<IActionResult> CreateFamily([FromBody] FamilyRequestCreate familyRequestCreate)
        {
            if (familyRequestCreate == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var domainFamily = _mapper.Map<Family>(familyRequestCreate);

            var createdFamily = await _familyService.CreateAsync(domainFamily);

            var response = _mapper.Map<FamilyResponse>(createdFamily);

            return CreatedAtAction(nameof(CreateFamily), new { id = response.Id }, response);
        }

        [HttpPost("family/register")]
        public async Task<IActionResult> RegisterFamily([FromBody] FamilyRegistrationRequestCreate familyRegistrationRequestCreate)
        {
            if (familyRegistrationRequestCreate == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var domainFamilyRegistration = _mapper.Map<FamilyRegistration>(familyRegistrationRequestCreate);

            var createdFamilyRegistration = await _familyService.RegisterAsync(domainFamilyRegistration);

            var response = _mapper.Map<FamilyRegistrationResponse>(createdFamilyRegistration);

            return CreatedAtAction(nameof(RegisterFamily), response);
        }

        [HttpPost("{familyId:Guid}/familymembers")]
        public async Task<IActionResult> CreateFamilyMember(Guid familyId, [FromBody] FamilyMemberRequestCreate familyMemberRequestCreate)
        {
            if (familyMemberRequestCreate == null)
            {
                return BadRequest();
            }

            if (familyId == Guid.Empty)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var domainFamilyMember = _mapper.Map<FamilyMember>(familyMemberRequestCreate);

            var createdFamilyMember = await _familyMemberService.CreateAsync(familyId, domainFamilyMember);

            var response = _mapper.Map<FamilyMemberResponse>(createdFamilyMember);

            return CreatedAtAction(nameof(CreateFamilyMember), new { id = response.Id }, response);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            await _familyService.DeleteAsync(id);

            return Ok();
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] FamilyRequestUpdate familyRequestUpdate)
        {
            if (familyRequestUpdate == null)
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

            var domainFamily = await _familyService.GetAsync(id);

            var updatedDomainFamily = _mapper.Map(familyRequestUpdate, domainFamily);

            await _familyService.UpdateAsync(updatedDomainFamily);

            return NoContent();
        }
    }
}