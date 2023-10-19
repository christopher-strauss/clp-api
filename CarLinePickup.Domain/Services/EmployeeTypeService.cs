using System.Threading.Tasks;
using System;
using AutoMapper;
using CarLinePickup.Domain.Services.Interfaces;
using Microsoft.Extensions.Options;
using CarLinePickup.Data.Models;
using CarLinePickup.Options.DomainOptions;
using Arch.EntityFrameworkCore.UnitOfWork;
using System.Collections.Generic;
using System.Linq;
using CarLinePickup.Domain.Extensions;
using CarLinePickup.Domain.Models.Interfaces;

namespace CarLinePickup.Domain.Services 
{
    public class EmployeeTypeService : ServiceBase<EmployeeType, Models.EmployeeType>, IEmployeeTypeService<Models.EmployeeType>
    {
        private readonly IRepository<EmployeeType> _employeeTypeRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly DomainOptions _options;

        public EmployeeTypeService(IOptions<DomainOptions> options, IMapper mapper,
            IUnitOfWork unitOfWork) : base(unitOfWork, mapper)
        {
            _employeeTypeRepository = unitOfWork.GetRepository<EmployeeType>();
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _options = options.Value;
        }

        #region Create
        #endregion

        #region Read
        public async Task<Models.PagedResult<Models.EmployeeType>> GetAllAsync(int pageSize, int pageIndex, string orderBy)
        {
            return await GetAllAsync(_employeeTypeRepository, pageSize, pageIndex, orderBy);
        }

        public async Task<Models.EmployeeType> GetAsync(Guid id)
        {
            return await GetAsync(id, _employeeTypeRepository);
        }

        public async Task<Models.EmployeeType> GetByNameAsync(string employeeTypeName)
        {
            var dataEmployeeType = await _employeeTypeRepository.GetFirstOrDefaultAsync(predicate: x => x.Description.ToLower() == employeeTypeName.ToLower());
            return _mapper.Map<Models.EmployeeType>(dataEmployeeType);
        }
        #endregion

        #region Update
        public async Task<Models.EmployeeType> UpdateAsync(Models.EmployeeType domainObject)
        {
            return await UpdateAsync(_employeeTypeRepository, domainObject);
        }
        #endregion

        #region Delete
        public async Task DeleteAsync(Guid id)
        {
            await DeleteAsync(id, _employeeTypeRepository);
        }
        #endregion
    }
}

