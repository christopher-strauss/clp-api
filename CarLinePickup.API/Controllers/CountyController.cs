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
    [Route("api/v{v:apiVersion}/counties")]
    public class CountyController : Controller
    {
        private readonly ICountyService<County> _countyService;
        private readonly IMapper _mapper;

        public CountyController(ICountyService<County> countyService, IMapper mapper)
        {
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
            var county = await _countyService.GetAsync(id);

            if (county == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<CountyResponse>(county);

            return Ok(response);
        }

        [HttpGet("{countyName}")]
        public async Task<IActionResult> Get(string countyName)
        {
            var county = await _countyService.GetByNameAsync(countyName);

            if (county == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<CountyResponse>(county);

            return Ok(response);
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllCounties([Required, FromQuery] int pageSize, [Required, FromQuery] int pageIndex, [Required, FromQuery] string orderBy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var counties = await _countyService.GetAllAsync(pageSize: pageSize, pageIndex: pageIndex, orderBy: orderBy);

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

            await _countyService.DeleteAsync(id);

            return Ok();
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CountyRequestUpdate countyRequestUpdate)
        {
            if (countyRequestUpdate == null)
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

            var domainCounty = await _countyService.GetAsync(id);

            var updatedDomainCounty = _mapper.Map(countyRequestUpdate, domainCounty);

            await _countyService.UpdateAsync(updatedDomainCounty);

            return NoContent();
        }
    }
}