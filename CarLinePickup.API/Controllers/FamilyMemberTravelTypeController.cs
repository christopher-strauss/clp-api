using AutoMapper;
using CarLinePickup.API.Models.Response;
using CarLinePickup.Domain.Models;
using CarLinePickup.Domain.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarLinePickup.API.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{v:apiVersion}/familymembertraveltypes")]
    public class FamilyMemberTravelTypeController : Controller
    {
        private readonly IFamilyMemberTravelTypeService<FamilyMemberTravelType> _familyMemberTravelTypeService;
        private readonly IValidatorFactory _validatorFactory;
        private readonly IMapper _mapper;

        public FamilyMemberTravelTypeController(IFamilyMemberTravelTypeService<FamilyMemberTravelType> familyMemberTravelTypeService, IMapper mapper, IValidatorFactory validatorFactory)
        {
            _familyMemberTravelTypeService = familyMemberTravelTypeService;
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
            var familyMemberTravelType = await _familyMemberTravelTypeService.GetAsync(id);

            if (familyMemberTravelType == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<FamilyMemberTravelTypeResponse>(familyMemberTravelType);

            return Ok(response);
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllFamilyMemberTravelTypes([Required, FromQuery] int pageSize, [Required, FromQuery] int pageIndex, [Required, FromQuery] string orderBy)
        {
            var familyMemberTravelTypes = await _familyMemberTravelTypeService.GetAllAsync(pageSize, pageIndex, orderBy);

            if (familyMemberTravelTypes == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<PagedResult<FamilyMemberTravelType>, PagedResponse<FamilyMemberTravelTypeResponse>>(familyMemberTravelTypes);

            return Ok(response);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            await _familyMemberTravelTypeService.DeleteAsync(id);

            return Ok();
        }
    }
}