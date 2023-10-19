using System;
using AutoMapper;
using CarLinePickup.API.Models.Response;
using CarLinePickup.Domain.Services.Interfaces;
using CarLinePickup.Domain.Models;
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
    [Route("api/v{v:apiVersion}/states")]
    public class StateController : Controller
    {
        private readonly IStateService<State> _stateService;
        private readonly ICountyService<County> _countyService;
        private readonly IMapper _mapper;

        public StateController(IStateService<State> stateService, ICountyService<County> countyService, IMapper mapper)
        {
            _stateService = stateService;
            _countyService = countyService;
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
            var state = await _stateService.GetAsync(id);

            if (state == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<StateResponse>(state);

            return Ok(response);
        }

        [HttpGet("{stateName}")]
        public async Task<IActionResult> Get(string stateName)
        {
            var state = await _stateService.GetByNameAsync(stateName);

            if (state == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<StateResponse>(state);

            return Ok(response);
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllStates([Required, FromQuery] int pageSize, [Required, FromQuery] int pageIndex, [Required, FromQuery] string orderBy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var states = await _stateService.GetAllAsync(pageSize: pageSize, pageIndex: pageIndex, orderBy: orderBy);

            if (states == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<PagedResult<State>, PagedResponse<StateResponse>>(states);

            return Ok(response);
        }

        [HttpGet("{stateAbbreviation}/counties")]
        public async Task<IActionResult> GetAllCountiesByStateAbbreviation(string stateAbbreviation, [Required, FromQuery] int pageSize, [Required, FromQuery] int pageIndex, [Required, FromQuery] string orderBy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var counties = await _countyService.GetAllCountiesByStateAbbreviationAsync(stateAbbreviation, pageSize: pageSize, pageIndex: pageIndex, orderBy: orderBy);

            if (counties == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<PagedResult<County>, PagedResponse<CountyResponse>>(counties);

            return Ok(response);
        }

        [HttpGet("{stateId:Guid}/counties")]
        public async Task<IActionResult> GetAllCountiesByStateId(Guid stateId, [Required, FromQuery] int pageSize, [Required, FromQuery] int pageIndex, [Required, FromQuery] string orderBy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var counties = await _countyService.GetAllCountiesByStateIdAsync(stateId, pageSize: pageSize, pageIndex: pageIndex, orderBy: orderBy);

            if (counties == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<PagedResult<County>, PagedResponse<CountyResponse>>(counties);

            return Ok(response);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            await _stateService.DeleteAsync(id);

            return Ok();
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] StateRequestUpdate stateRequestUpdate)
        {
            if (stateRequestUpdate == null)
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

            var domainState = await _stateService.GetAsync(id);

            var updatedDomainState = _mapper.Map(stateRequestUpdate, domainState);

            await _stateService.UpdateAsync(updatedDomainState);

            return NoContent();
        }
    }
}