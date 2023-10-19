using System;
using AutoMapper;
using FluentValidation;
using CarLinePickup.API.Models.Response;
using CarLinePickup.Domain.Services.Interfaces;
using CarLinePickup.API.Models.Request.Create;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using CarLinePickup.API.Models.Request.Update;
using CarLinePickup.Domain.Models;

namespace CarLinePickup.API.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{v:apiVersion}/grades")]
    public class GradeController : Controller
    {
        private readonly IGradeService<Grade> _gradeService;
        private readonly IValidatorFactory _validatorFactory;
        private readonly IMapper _mapper;

        public GradeController(IGradeService<Grade> gradeService, IMapper mapper, IValidatorFactory validatorFactory)
        {
            _gradeService = gradeService;
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
            var grade = await _gradeService.GetAsync(id);

            if (grade == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<GradeResponse>(grade);

            return Ok(response);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }
            
            await _gradeService.DeleteAsync(id);

            return Ok();
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> UpdateGrade(Guid id, [FromBody] GradeRequestUpdate gradeRequestUpdate)
        {
            if (gradeRequestUpdate == null)
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

            var domainGrade = await _gradeService.GetAsync(id);

            var updatedDomainGrade = _mapper.Map(gradeRequestUpdate, domainGrade);

            await _gradeService.UpdateAsync(updatedDomainGrade);

            return NoContent();

        }
    }
}
