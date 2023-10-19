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
    [Route("api/v{v:apiVersion}/familymembertypes")]
    public class FamilyMemberTypeController : Controller
    {
        private readonly IFamilyMemberTypeService<FamilyMemberType> _familyMemberTypeService;
        private readonly IValidatorFactory _validatorFactory;
        private readonly IMapper _mapper;

        public FamilyMemberTypeController(IFamilyMemberTypeService<FamilyMemberType> familyMemberTypeService, IMapper mapper, IValidatorFactory validatorFactory)
        {
            _familyMemberTypeService = familyMemberTypeService;
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
            var familyMemberType = await _familyMemberTypeService.GetAsync(id);

            if (familyMemberType == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<FamilyMemberTypeResponse>(familyMemberType);

            return Ok(response);
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllFamilyMemberTypes([Required, FromQuery] int pageSize, [Required, FromQuery] int pageIndex, [Required, FromQuery] string orderBy)
        {
            var familyMemberTypes = await _familyMemberTypeService.GetAllAsync(pageSize, pageIndex, orderBy);

            if (familyMemberTypes == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<PagedResult<FamilyMemberType>, PagedResponse<FamilyMemberTypeResponse>>(familyMemberTypes);

            return Ok(response);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            await _familyMemberTypeService.DeleteAsync(id);

            return Ok();
        }
    }
}