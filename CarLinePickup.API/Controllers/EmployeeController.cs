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
    [Route("api/v{v:apiVersion}/employees")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService<Employee> _employeeService;
        private readonly IValidatorFactory _validatorFactory;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeService<Employee> employeeService, IMapper mapper, IValidatorFactory validatorFactory)
        {
            _employeeService = employeeService;
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
            var employee = await _employeeService.GetAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<EmployeeResponse>(employee);

            return Ok(response);
        }

        [HttpGet("{emailAddress}")]
        public async Task<IActionResult> GetEmployeeByEmailAddress(string emailAddress)
        {
            var employee = await _employeeService.GetByEmailAddressAsync(emailAddress);

            if (employee == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<EmployeeResponse>(employee);

            return Ok(response);
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllEmployees([Required, FromQuery] int pageSize, [Required, FromQuery] int pageIndex, [Required, FromQuery] string orderBy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employees = await _employeeService.GetAllAsync(pageSize: pageSize, pageIndex: pageIndex, orderBy: orderBy);

            if (employees == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<PagedResult<Employee>, PagedResponse<EmployeeResponse>>(employees);

            return Ok(response);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            await _employeeService.DeleteAsync(id);

            return Ok();
        }

        [HttpPut("{id:Guid}/statuses/{status}")]
        public async Task<IActionResult> UpdateStatus(Guid id, string status)
        {  
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            if (string.IsNullOrEmpty(status))
            {
                return BadRequest(status);
            }

            if (!string.Equals(status, "enabled", StringComparison.OrdinalIgnoreCase) && !string.Equals(status, "disabled", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest(status);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var domainEmployee = await _employeeService.GetAsync(id);

            await _employeeService.UpdateStatus(domainEmployee, status);

            return NoContent();
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] EmployeeRequestUpdate employeeRequestUpdate)
        {
            if (employeeRequestUpdate == null)
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

            var domainEmployee = await _employeeService.GetAsync(id);

            var updatedDomainEmployee = _mapper.Map(employeeRequestUpdate, domainEmployee);

            await _employeeService.UpdateAsync(updatedDomainEmployee);

            return NoContent();
        }
    }
}