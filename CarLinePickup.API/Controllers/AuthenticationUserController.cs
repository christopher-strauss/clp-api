using System;
using AutoMapper;
using CarLinePickup.API.Models.Response;
using CarLinePickup.Domain.Models;
using CarLinePickup.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarLinePickup.API.Models.Request.Update;
using CarLinePickup.API.Models.Request.Create;
using FluentValidation;
using CarLinePickup.Domain.Services;
using System.ComponentModel.DataAnnotations;

namespace CarLinePickup.API.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{v:apiVersion}/authenticationusers")]
    public class AuthenticationUserController : Controller
    {
        private readonly IAuthenticationUserService<AuthenticationUser> _authenticationUserService;
        private readonly IAuthenticationUserTypeService<AuthenticationUserType> _authenticationUserTypeService; 
        private readonly IMapper _mapper;
        private readonly IValidatorFactory _validatorFactory;
        public AuthenticationUserController(IAuthenticationUserService<AuthenticationUser> authenticationUserService, IAuthenticationUserTypeService<AuthenticationUserType> authenticationUserTypeService, IMapper mapper, IValidatorFactory validatorFactory)
        {
            _authenticationUserService = authenticationUserService;
            _authenticationUserTypeService = authenticationUserTypeService;
            _mapper = mapper;
            _validatorFactory = validatorFactory;
        }

        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok();
        }

        [HttpGet("authenticationuser/externalproviders/{externalProviderId}/externalprovidertypes/{providerType}")]
        public async Task<IActionResult> Get(string externalProviderId, string providerType)
        {
            var authenticationUser = await _authenticationUserService.GetByExternalIdAndProviderTypeAsync(externalProviderId, providerType);

            if (authenticationUser == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<AuthenticationUserResponse>(authenticationUser);

            return Ok(response);
        }

        [HttpGet("authenticationuser/types")]
        public async Task<IActionResult> GetAuthenticationUserTypes([Required, FromQuery] int pageSize, [Required, FromQuery] int pageIndex, [Required, FromQuery] string orderBy)
        {
            var authenticationUserTypes = await _authenticationUserTypeService.GetAllAsync(pageSize: pageSize, pageIndex: pageIndex, orderBy: orderBy);

            if (authenticationUserTypes == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<PagedResult<AuthenticationUserType>, PagedResponse<AuthenticationUserTypeResponse>>(authenticationUserTypes);

            return Ok(response);
        }

        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] AuthenticationUserRequestCreate authenticationUserRequestCreate)
        {
            if (authenticationUserRequestCreate == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            var domainAuthenticationUser = _mapper.Map<AuthenticationUser>(authenticationUserRequestCreate);

            var createdAuthenticationUser = await _authenticationUserService.CreateAsync(domainAuthenticationUser);

            var response = _mapper.Map<AuthenticationUserResponse>(createdAuthenticationUser);

            return CreatedAtAction(nameof(Create), new { id = response.Id }, response);
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] AuthenticationUserRequestUpdate authenticationUserRequestUpdate)
        {
            if (authenticationUserRequestUpdate == null)
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

            var domainAuthenticationUser = await _authenticationUserService.GetAsync(id);

            var updatedAuthenticationUser = _mapper.Map(authenticationUserRequestUpdate, domainAuthenticationUser);

            await _authenticationUserService.UpdateAsync(updatedAuthenticationUser);

            return NoContent();
        }
    }
}