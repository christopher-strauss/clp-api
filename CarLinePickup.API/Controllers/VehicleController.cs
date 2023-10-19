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
using CarLinePickup.Domain.Models.VPIC;
using CarLinePickup.API.Models.Request.Update;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CarLinePickup.API.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{v:apiVersion}/vehicles")]
    public class VehicleController : Controller
    {
        private readonly IVehicleService<Vehicle> _vehicleService;
        private readonly IMapper _mapper;

        public VehicleController(IVehicleService<Vehicle> vehicleService, IMapper mapper)
        {
            _vehicleService = vehicleService;
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
            var vehicle = await _vehicleService.GetAsync(id);

            if (vehicle == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<VehicleResponse>(vehicle);

            return Ok(response);
        }

        [HttpGet("makes")]
        public async Task<IActionResult> GetMakes([Required, FromQuery] int pageSize, [Required, FromQuery] int pageIndex, [Required, FromQuery] string orderBy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var makes = await _vehicleService.GetVehicleMakesAsync(pageSize: pageSize, pageIndex: pageIndex, orderBy: orderBy);

            if (makes == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<PagedResult<VehicleMake>, PagedResponse<VehicleMakeResponse>>(makes);

            return Ok(response);
        }

        [HttpGet("makes/{id}/models")]
        public async Task<IActionResult> GetModelsByVehicleId(Guid id, [Required, FromQuery] int pageSize, [Required, FromQuery] int pageIndex, [Required, FromQuery] string orderBy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var models = await _vehicleService.GetVehicleModelsByMakeIdAsync(id, pageSize: pageSize, pageIndex: pageIndex, orderBy: orderBy);

            if (models == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<PagedResult<VehicleModel>, PagedResponse<VehicleModelResponse>>(models);

            return Ok(response);
        }

        [HttpGet("makes/{id}/years/{year}/models")]
        public async Task<IActionResult> GetVehicleModelsByVehicleIdAndYear(Guid id, string year, [Required, FromQuery] int pageSize, [Required, FromQuery] int pageIndex, [Required, FromQuery] string orderBy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var models = await _vehicleService.GetVehicleModelsByMakeIdAndYearAsync(id, year, pageSize: pageSize, pageIndex: pageIndex, orderBy: orderBy);

            if (models == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<PagedResult<VehicleModel>, PagedResponse<VehicleModelResponse>>(models);

            return Ok(response);
        }

        [HttpGet("makes/{id}/years")]
        public async Task<IActionResult> GetModelYearsForMakeAsync(Guid id)
        {
            var years = await _vehicleService.GetModelYearsForMakeAsync(id);

            if (years == null)
            {
                return NotFound();
            }

            return Ok(years);
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllVehicles([Required, FromQuery] int pageSize, [Required, FromQuery] int pageIndex, [Required, FromQuery] string orderBy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vehicles = await _vehicleService.GetAllAsync(pageSize: pageSize, pageIndex: pageIndex, orderBy: orderBy);

            if (vehicles == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<PagedResult<Vehicle>, PagedResponse<VehicleResponse>>(vehicles);

            return Ok(response);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            await _vehicleService.DeleteAsync(id);

            return Ok();
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] VehicleRequestUpdate vehicleRequestUpdate)
        {
            if (vehicleRequestUpdate == null)
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

            var domainVehicle = await _vehicleService.GetAsync(id);

            var updatedDomainVehicle = _mapper.Map(vehicleRequestUpdate, domainVehicle);

            await _vehicleService.UpdateAsync(updatedDomainVehicle);

            return NoContent();
        }
    }
}