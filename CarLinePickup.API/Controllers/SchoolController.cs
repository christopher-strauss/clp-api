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
using CarLinePickup.Domain.Models;
using FluentValidation;
using CarLinePickup.API.Models.Request.Update;
using System.ComponentModel.DataAnnotations;

namespace CarLinePickup.API.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{v:apiVersion}/schools")]
    public class SchoolController : Controller
    {
        private readonly ISchoolService<School> _schoolService;
        private readonly IGradeService<Grade> _gradeService;
        private readonly IEmployeeService<Employee> _employeeService;
        private readonly IValidatorFactory _validatorFactory;
        private readonly IMapper _mapper;

        public SchoolController(ISchoolService<School> schoolService, IGradeService<Grade> gradeServcie, IEmployeeService<Employee> employeeServcie, IMapper mapper, IValidatorFactory validatorFactory)
        {
            _schoolService = schoolService;
            _gradeService = gradeServcie;
            _employeeService = employeeServcie;
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
            var school = await _schoolService.GetAsync(id);

            if (school == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<SchoolResponse>(school);

            return Ok(response);
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllSchools([Required, FromQuery] int pageSize, [Required, FromQuery] int pageIndex, [Required, FromQuery] string orderBy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var schools = await _schoolService.GetAllAsync(pageSize: pageSize, pageIndex: pageIndex, orderBy: orderBy);

            if (schools == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<PagedResult<School>, PagedResponse<SchoolResponse>>(schools);

            return Ok(response);
        }

        [HttpPost("{schoolId}/grades")]
        public async Task<IActionResult> CreateGrade(Guid schoolId, [FromBody] GradeRequestCreate gradeRequestCreate)
        {
            if (gradeRequestCreate == null)
            {
                return BadRequest();
            }

            if (schoolId == Guid.Empty)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var domainGrade = _mapper.Map<Grade>(gradeRequestCreate);

            var createdGrade = await _gradeService.CreateAsync(schoolId, domainGrade);

            var response = _mapper.Map<GradeResponse>(createdGrade);

            return CreatedAtAction(nameof(CreateGrade), new { id = response.Id }, response);
        }

        [HttpPost()]
        public async Task<IActionResult> CreateSchool([FromBody] SchoolRequestCreate schoolRequestCreate)
        {
            if (schoolRequestCreate == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var domainSchool = _mapper.Map<School>(schoolRequestCreate);

            var createdSchool = await _schoolService.CreateAsync(domainSchool);

            var response = _mapper.Map<SchoolResponse>(createdSchool);

            return CreatedAtAction(nameof(CreateSchool), new { id = response.Id }, response);
        }

        [HttpGet("{schoolId}/employees")]
        public async Task<IActionResult> GetEmployeesBySchoolId(Guid schoolId, [Required, FromQuery] int pageSize, [Required, FromQuery] int pageIndex, [Required, FromQuery] string orderBy)
        {
            if (schoolId == Guid.Empty)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employees = await _employeeService.GetEmployeesBySchoolId(schoolId, pageSize: pageSize, pageIndex: pageIndex, orderBy: orderBy);

            if (employees == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<PagedResult<Employee>, PagedResponse<EmployeeResponse>>(employees);

            return Ok(response);
        }

        [HttpPost("{schoolId}/employees")]
        public async Task<IActionResult> CreateEmployee(Guid schoolId, [FromBody] EmployeeRequestCreate employeeRequestCreate)
        {
            if (employeeRequestCreate == null)
            {
                return BadRequest();
            }

            if (schoolId == Guid.Empty)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var domainEmployee = _mapper.Map<Employee>(employeeRequestCreate);

            var createdEmployee = await _employeeService.CreateAsync(schoolId, domainEmployee);

            var response = _mapper.Map<EmployeeResponse>(createdEmployee);

            return CreatedAtAction(nameof(CreateEmployee), new { id = response.Id }, response);
        }

        /*
        [HttpPost("{schoolId}/employees/employee/register")]
        public async Task<IActionResult> RegisterEmployee(Guid schoolId, [FromBody] EmployeeRegistrationRequestCreate employeeRegistrationRequestCreate)
        {
            if (employeeRegistrationRequestCreate == null)
            {
                return BadRequest();
            }

            if (schoolId == Guid.Empty)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var domainEmployee = _mapper.Map<Employee>(employeeRequestCreate);

            //var registeredEmployee = await _employeeService.CreateAsync(schoolId, domainEmployee);

            //var response = _mapper.Map<EmployeeRegistrationResponse>(createdEmployee);

            return CreatedAtAction(nameof(RegisterEmployee), response);
        }
        */

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            await _schoolService.DeleteAsync(id);

            return Ok();
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] SchoolRequestUpdate schoolRequestUpdate)
        {
            if (schoolRequestUpdate == null)
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

            var domainSchool = await _schoolService.GetAsync(id);

            var updatedDomainSchool = _mapper.Map(schoolRequestUpdate, domainSchool);

            await _schoolService.UpdateAsync(updatedDomainSchool);

            return NoContent();
        }
    }
}