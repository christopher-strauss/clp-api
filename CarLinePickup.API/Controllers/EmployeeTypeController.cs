using System;
using AutoMapper;
using FluentValidation;
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
    [Route("api/v{v:apiVersion}/employeetypes")]
    public class EmployeeTypeController : Controller
    {
        private readonly IEmployeeTypeService<EmployeeType> _employeeTypeService;
        private readonly IValidatorFactory _validatorFactory;
        private readonly IMapper _mapper;

        public EmployeeTypeController(IEmployeeTypeService<EmployeeType> employeeTypeService, IMapper mapper, IValidatorFactory validatorFactory)
        {
            _employeeTypeService = employeeTypeService;
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
            var employeeType = await _employeeTypeService.GetAsync(id);

            if (employeeType == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<EmployeeTypeResponse>(employeeType);

            return Ok(response);
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllEmployeeTypes([Required, FromQuery] int pageSize, [Required, FromQuery] int pageIndex, [Required, FromQuery] string orderBy)
        {
            var employeeTypes = await _employeeTypeService.GetAllAsync(pageSize, pageIndex, orderBy);

            if (employeeTypes == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<PagedResult<EmployeeType>, PagedResponse<EmployeeTypeResponse>>(employeeTypes);

            return Ok(response);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            await _employeeTypeService.DeleteAsync(id);

            return Ok();
        }
    }
}