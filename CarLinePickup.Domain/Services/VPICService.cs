using System;
using System.Threading.Tasks;
using AutoMapper;
using Flurl;
using Flurl.Http;
using CarLinePickup.Domain.Services.Interfaces;
using Microsoft.Extensions.Options;
using CarLinePickup.Data.Models;
using CarLinePickup.Options.DomainOptions;
using Arch.EntityFrameworkCore.UnitOfWork;
using System.Collections.Generic;
using System.Linq;
using CarLinePickup.Domain.Services.Comparers;
using System.Globalization;
using System.Threading;

namespace CarLinePickup.Domain.Services
{
    public class VPICService : IVPICService
    {
        private readonly IMapper _mapper;
        private readonly DomainOptions _options;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<VehicleMake> _vehicleMakeRepository;
        private readonly IRepository<VehicleModel> _vehicleModelRepository;

        public VPICService(IOptions<DomainOptions> options, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _vehicleMakeRepository = unitOfWork.GetRepository<VehicleMake>();
            _vehicleModelRepository = unitOfWork.GetRepository<VehicleModel>();
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _options = options.Value;
        }

        private async Task<IList<Models.VPIC.Make>> GetVehicleMakes()
        {
            var cars = await _options.VPICOptions.BaseUrl
             .AppendPathSegment(_options.VPICOptions.UrlSegment)
             .AppendPathSegment("/GetMakesForVehicleType/car")
             .SetQueryParam("format=json")
             .WithHeader("Content-Type", "application/json")
             .GetJsonAsync<Models.VPIC.MakeResult>();

            var trucks = await _options.VPICOptions.BaseUrl
             .AppendPathSegment(_options.VPICOptions.UrlSegment)
             .AppendPathSegment("/GetMakesForVehicleType/truck")
             .SetQueryParam("format=json")
             .WithHeader("Content-Type", "application/json")
             .GetJsonAsync<Models.VPIC.MakeResult>();
    
            var vehicleList = trucks.Makes.Union(cars.Makes, new MakeComparer()).OrderBy(x => x.Name).ToList();

            vehicleList.ForEach(x => x.Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(x.Name.ToLower()));
            
            return vehicleList;
        }

        private async Task<IList<Models.VPIC.Model>> GetVehicleModelsByMakeId(int id)
        {
            var response = await _options.VPICOptions.BaseUrl
             .AppendPathSegment(_options.VPICOptions.UrlSegment)
             .AppendPathSegment($"/GetModelsForMakeId/{id}")
             .SetQueryParam("format=json")
             .WithHeader("Content-Type", "application/json")
             .GetJsonAsync<Models.VPIC.ModelResult>();

            return response.Models.OrderBy(x => x.Name).ToList();
        }

        private async Task<IList<Models.VPIC.Model>> GetVehicleModelsByMakeIdAndYear(int id, int year)
        {
            var cars = await _options.VPICOptions.BaseUrl
             .AppendPathSegment(_options.VPICOptions.UrlSegment)
             .AppendPathSegment($"/GetModelsForMakeIdYear/makeId/{id}/modelyear/{year}/vehicletype/car")
             .SetQueryParam("format=json")
             .WithHeader("Content-Type", "application/json")
             .GetJsonAsync<Models.VPIC.ModelResult>();

            var mpv = await _options.VPICOptions.BaseUrl
             .AppendPathSegment(_options.VPICOptions.UrlSegment)
             .AppendPathSegment($"/GetModelsForMakeIdYear/makeId/{id}/modelyear/{year}/vehicletype/Multipurpose Passenger Vehicle (MPV)")
             .SetQueryParam("format=json")
             .WithHeader("Content-Type", "application/json")
             .GetJsonAsync<Models.VPIC.ModelResult>();

            var trucks = await _options.VPICOptions.BaseUrl
             .AppendPathSegment(_options.VPICOptions.UrlSegment)
             .AppendPathSegment($"/GetModelsForMakeIdYear/makeId/{id}/modelyear/{year}/vehicletype/truck")
             .SetQueryParam("format=json")
             .WithHeader("Content-Type", "application/json")
             .GetJsonAsync<Models.VPIC.ModelResult>();

            //Combine the Cars and MPVs
            cars.Models = cars.Models.Concat(mpv.Models).ToList();

            var vehicleList = trucks.Models.Union(cars.Models, new ModelComparer()).OrderBy(x => x.Name).ToList();

            vehicleList.ForEach(x => x.Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(x.Name.ToLower()));

            return vehicleList;
        }

        private async Task CreateVehicleMakesAndModels()
        {
            int vehicleYear = 1980;

            var vehicleMakesLocal = await _vehicleMakeRepository.GetPagedListAsync(pageSize: 2000);
            var vehicleMakesVPIC = await GetVehicleMakes();


            var makeList = vehicleMakesVPIC.Where(p => !vehicleMakesLocal.Items.Any(e => e.ExternalId == p.Id)).ToList();


            //Loop thru all the makes that are returned from the VPIC service call
            foreach (var vpicMake in vehicleMakesVPIC)
            {
                //check to see if the make exists in the local database
                bool makeExists = vehicleMakesLocal.Items.Any(m => m.ExternalId == vpicMake.Id);


                //if the make does not exist add it to the database
                if (!makeExists)
                {
                    var dataVehicleMake = new VehicleMake();
                    dataVehicleMake.ExternalId = vpicMake.Id;
                    dataVehicleMake.Name = vpicMake.Name;
                    dataVehicleMake.CreatedBy = "VPIC";

                    await _vehicleMakeRepository.InsertAsync(dataVehicleMake);
                    //Persist the changes
                    await _unitOfWork.SaveChangesAsync();
                }

                var vehicleMakeLocal = await _vehicleMakeRepository.GetFirstOrDefaultAsync(predicate: x => x.ExternalId == vpicMake.Id);


                //loop through all the years until current
                for (int i = vehicleYear; i <= DateTime.Now.Year; i++)
                {

                    var vehicleModelsLocal = await _vehicleModelRepository.GetPagedListAsync(predicate: x => x.ExternalId == vpicMake.Id && x.Year == i.ToString(), pageSize: 2000);

                    //Get all the vehicle models by make id and year
                    var vehicleModelsVPIC = await GetVehicleModelsByMakeIdAndYear(vpicMake.Id, i);

                    foreach (var vpicModel in vehicleModelsVPIC)
                    {
                        //check to see if the model exists in the local database
                        bool modelExists = vehicleModelsLocal.Items.Any(m => m.ExternalId == vpicModel.Id);

                        //if the model does not exist add it to the database
                        if (!modelExists)
                        {

                            var dataVehicleModel = new VehicleModel();

                            dataVehicleModel.VehicleMakeId = vehicleMakeLocal.Id;
                            dataVehicleModel.ExternalId = vpicModel.Id;
                            dataVehicleModel.Name = vpicModel.Name;
                            dataVehicleModel.Year = i.ToString();
                            dataVehicleModel.CreatedBy = "VPIC";

                            await _vehicleModelRepository.InsertAsync(dataVehicleModel);
                        }
                    }

                    //Persist the changes
                    await _unitOfWork.SaveChangesAsync();

                    Thread.Sleep(3000);
                }
                

            }
        }

    }
}