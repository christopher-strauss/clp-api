using System;
using AutoMapper;
using CarLinePickup.API.Models.Response;
using CarLinePickup.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarLinePickup.Domain.Models;
using CarLinePickup.API.Models.Request.Update;
using System.ComponentModel.DataAnnotations;

namespace CarLinePickup.API.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{v:apiVersion}/familymembers")]
    public class FamilyMemberController : Controller
    {
        private readonly IFamilyMemberService<FamilyMember> _familyMemberService;
        private readonly IMapper _mapper;

        public FamilyMemberController(IFamilyMemberService<FamilyMember> familyMemberService, IMapper mapper)
        {
            _familyMemberService = familyMemberService;
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
            var familyMember = await _familyMemberService.GetAsync(id);

            if (familyMember == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<FamilyMemberResponse>(familyMember);

            return Ok(response);
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllFamilyMembers([Required, FromQuery] int pageSize, [Required, FromQuery] int pageIndex, [Required, FromQuery] string orderBy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var familyMembers = await _familyMemberService.GetAllAsync(pageSize: pageSize, pageIndex: pageIndex, orderBy: orderBy);

            if (familyMembers == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<PagedResult<FamilyMember>, PagedResponse<FamilyMemberResponse>>(familyMembers);

            return Ok(response);
        }

        [HttpGet("{emailAddress}")]
        public async Task<IActionResult> GetFamilyMemberByEmailAddress(string emailAddress)
        {
            var familyMember = await _familyMemberService.GetByEmailAddressAsync(emailAddress);

            if (familyMember == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<FamilyMemberResponse>(familyMember);

            return Ok(response);
        }

        [HttpGet("familymembertypes/{familyMemberTypeId:Guid}")]
        public async Task<IActionResult> GetFamilyMembersByTypeId(Guid familyMemberTypeId, [Required, FromQuery] int pageSize, [Required, FromQuery] int pageIndex, [Required, FromQuery] string orderBy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var familyMembers = await _familyMemberService.GetAllFamilyMembersByTypeIdAsync(familyMemberTypeId, pageSize: pageSize, pageIndex: pageIndex, orderBy: orderBy);

            if (familyMembers == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<PagedResult<FamilyMember>, PagedResponse<FamilyMemberResponse>>(familyMembers);

            return Ok(response);
        }

        [HttpGet("familymembertypes/{familyMemberType}")]
        public async Task<IActionResult> GetFamilyMembersByType(string familyMemberType, [Required, FromQuery] int pageSize, [Required, FromQuery] int pageIndex, [Required, FromQuery] string orderBy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var familyMembers = await _familyMemberService.GetAllFamilyMembersByTypeAsync(familyMemberType, pageSize: pageSize, pageIndex: pageIndex, orderBy: orderBy);

            if (familyMembers == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<PagedResult<FamilyMember>, PagedResponse<FamilyMemberResponse>>(familyMembers);

            return Ok(response);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            await _familyMemberService.DeleteAsync(id);

            return Ok();
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] FamilyMemberRequestUpdate familyMemberRequestUpdate)
        {
            if (familyMemberRequestUpdate == null)
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

            var domainFamilyMember = await _familyMemberService.GetAsync(id);

            var updatedDomainFamilyMember = _mapper.Map(familyMemberRequestUpdate, domainFamilyMember);

            await _familyMemberService.UpdateAsync(updatedDomainFamilyMember);

            return NoContent();
        }
    }
}