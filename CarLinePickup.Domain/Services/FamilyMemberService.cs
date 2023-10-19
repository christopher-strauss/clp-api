using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CarLinePickup.Domain.Services.Interfaces;
using Microsoft.Extensions.Options; 
using CarLinePickup.Options.DomainOptions;
using Arch.EntityFrameworkCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using CarLinePickup.Domain.Extensions;
using CarLinePickup.Data.Models;
using CarLinePickup.Domain.Models.Interfaces;
using Microsoft.EntityFrameworkCore.Query;

namespace CarLinePickup.Domain.Services
{
    public class FamilyMemberService : ServiceBase<FamilyMember, Models.FamilyMember>, IFamilyMemberService<Models.FamilyMember>
    {
        private readonly IRepository<FamilyMember> _familyMemberRepository;
        private readonly IRepository<FamilyMemberType> _familyMemberTypeRepository;
        private readonly IRepository<FamilyMemberTravelType> _familyMemberTravelTypeRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly DomainOptions _options;
        public FamilyMemberService(IOptions<DomainOptions> options, IMapper mapper,
            IUnitOfWork unitOfWork) : base(unitOfWork, mapper)
        {
            _familyMemberRepository = unitOfWork.GetRepository<FamilyMember>();
            _familyMemberTypeRepository = unitOfWork.GetRepository<FamilyMemberType>();
            _familyMemberTravelTypeRepository = unitOfWork.GetRepository<FamilyMemberTravelType>();
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _options = options.Value;
        }

        #region Create
        public async Task<Models.FamilyMember> CreateAsync(Guid familyId, Models.FamilyMember familyMember)
        {
            FamilyMember dataFamilyMember = new FamilyMember();

            dataFamilyMember = _mapper.Map<FamilyMember>(familyMember);

            dataFamilyMember.CreatedDate = DateTime.Now;
            dataFamilyMember.ModifiedDate = DateTime.Now;

            var dataFamilyMemberType = await _familyMemberTypeRepository.GetFirstOrDefaultAsync(predicate: x => x.Id == familyMember.FamilyMemberTypeId) ??
                throw new KeyNotFoundException($"There is no {nameof(FamilyMemberType)} with Id: { familyMember.FamilyMemberTypeId }");

            FamilyMemberTravelType dataFamilyMemberTravelType = null;

            if (familyMember.FamilyMemberTravelTypeId.HasValue)
            {
                dataFamilyMemberTravelType = await _familyMemberTravelTypeRepository.GetFirstOrDefaultAsync(predicate: x => x.Id == familyMember.FamilyMemberTravelTypeId) ??
                   throw new KeyNotFoundException($"There is no {nameof(FamilyMemberTravelType)} with id: { familyMember.FamilyMemberTravelTypeId }");

                dataFamilyMember.FamilyMemberTravelTypeId = dataFamilyMemberTravelType.Id;
            }

            dataFamilyMember.FamilyId = familyId;
            dataFamilyMember.FamilyMemberTypeId = dataFamilyMemberType.Id;

            dataFamilyMember.Family = null;

            await _familyMemberRepository.InsertAsync(dataFamilyMember);

            await _unitOfWork.SaveChangesAsync();

            return await GetAsync(dataFamilyMember.Id);
        }
        #endregion

        #region Read
        public async Task<Models.FamilyMember> GetAsync(Guid id)
        {
            Func<IQueryable<FamilyMember>, IIncludableQueryable<FamilyMember, object>> include = source => source.Include(familyMember => familyMember.FamilyMemberType)
                            .Include(familyMember => familyMember.FamilyMemberTravelType)
                            .Include(familyMember => familyMember.EmployeeGrade)
                                .ThenInclude(eg => eg.Employee)
                                .ThenInclude(e => e.School)
                            .Include(familyMember => familyMember.EmployeeGrade)
                                .ThenInclude(eg => eg.Grade);

            return await GetAsync(id, _familyMemberRepository, include);
        }

        public async Task<Models.FamilyMember> GetByEmailAddressAsync(string emailAddress)
        {
            Func<IQueryable<FamilyMember>, IIncludableQueryable<FamilyMember, object>> include = source => source.Include(familyMember => familyMember.FamilyMemberType)
                                                                                                                 .Include(familymember => familymember.FamilyMemberTravelType)
                                                                                                                .Include(familyMember => familyMember.EmployeeGrade)
                                                                                                                    .ThenInclude(eg => eg.Employee)
                                                                                                                    .ThenInclude(e => e.School)
                                                                                                                .Include(familyMember => familyMember.EmployeeGrade)
                                                                                                                    .ThenInclude(eg => eg.Grade);

            var familyMember = await _familyMemberRepository.GetFirstOrDefaultAsync(include: include, predicate: x => x.Email == emailAddress);

            return _mapper.Map<Models.FamilyMember>(familyMember);
        }

        public async Task<Models.PagedResult<Models.FamilyMember>> GetFamilyMembersByFamilyIdAsync(Guid familyId, int pageSize, int pageIndex, string orderBy)
        {

            IList<Models.FamilyMember> domainFamilyMembers = new List<Models.FamilyMember>();

            Func<IQueryable<FamilyMember>, IIncludableQueryable<FamilyMember, object>> include = source => source.Include(familymember => familymember.FamilyMemberType)
                                                                                                        .Include(familymember => familymember.FamilyMemberTravelType)
                                                                                                        .Include(familyMember => familyMember.EmployeeGrade)
                                                                                                            .ThenInclude(eg => eg.Employee)
                                                                                                            .ThenInclude(e => e.School)
                                                                                                        .Include(familyMember => familyMember.EmployeeGrade)
                                                                                                            .ThenInclude(eg => eg.Grade);

            var familyMembers = await _familyMemberRepository.GetPagedListAsync(include: include, pageIndex: pageIndex, pageSize: pageSize, orderBy: x => x.OrderBy(orderBy), predicate: x => x.FamilyId == familyId);


            familyMembers.Items.ToList().ForEach(fm => domainFamilyMembers.Add(_mapper.Map<Models.FamilyMember>(fm)));

            var result = new Models.PagedResult<Models.FamilyMember>(domainFamilyMembers, pageIndex, pageSize, familyMembers.TotalCount, familyMembers.TotalPages);

            return result;
        }

        public async Task<Models.PagedResult<Models.FamilyMember>> GetFamilyMembersByFamilyIdAndTypeAsync(Guid familyId, string familyMemberType, int pageSize, int pageIndex, string orderBy)
        {

            IList<Models.FamilyMember> domainFamilyMembers = new List<Models.FamilyMember>();

            Func<IQueryable<FamilyMember>, IIncludableQueryable<FamilyMember, object>> include = source => source.Include(familymember => familymember.FamilyMemberType)
                                                                                                                 .Include(familymember => familymember.FamilyMemberTravelType)
                                                                                                                .Include(familyMember => familyMember.EmployeeGrade)
                                                                                                                    .ThenInclude(eg => eg.Employee)
                                                                                                                    .ThenInclude(e => e.School)
                                                                                                                .Include(familyMember => familyMember.EmployeeGrade)
                                                                                                                    .ThenInclude(eg => eg.Grade);

            var familyMembers = await _familyMemberRepository.GetPagedListAsync(include: include, pageIndex: pageIndex, pageSize: pageSize, orderBy: x => x.OrderBy(orderBy), predicate: x => x.FamilyId == familyId && x.FamilyMemberType.Description == familyMemberType);

            familyMembers.Items.ToList().ForEach(fm => domainFamilyMembers.Add(_mapper.Map<Models.FamilyMember>(fm)));

            var result = new Models.PagedResult<Models.FamilyMember>(domainFamilyMembers, pageIndex, pageSize, familyMembers.TotalCount, familyMembers.TotalPages);

            return result;
        }

        public async Task<Models.PagedResult<Models.FamilyMember>> GetFamilyMembersByFamilyIdAndTypeIdAsync(Guid familyId, Guid familyMemberTypeId, int pageSize, int pageIndex, string orderBy)
        {

            IList<Models.FamilyMember> domainFamilyMembers = new List<Models.FamilyMember>();

            Func<IQueryable<FamilyMember>, IIncludableQueryable<FamilyMember, object>> include = source => source.Include(familymember => familymember.FamilyMemberType)
                                                                                                                 .Include(familymember => familymember.FamilyMemberTravelType)
                                                                                                                .Include(familyMember => familyMember.EmployeeGrade)
                                                                                                                    .ThenInclude(eg => eg.Employee)
                                                                                                                    .ThenInclude(e => e.School)
                                                                                                                .Include(familyMember => familyMember.EmployeeGrade)
                                                                                                                    .ThenInclude(eg => eg.Grade);

            var familyMembers = await _familyMemberRepository.GetPagedListAsync(include: include, pageIndex: pageIndex, pageSize: pageSize, orderBy: x => x.OrderBy(orderBy), predicate: x => x.FamilyId == familyId && x.FamilyMemberTypeId == familyMemberTypeId);

            familyMembers.Items.ToList().ForEach(fm => domainFamilyMembers.Add(_mapper.Map<Models.FamilyMember>(fm)));

            var result = new Models.PagedResult<Models.FamilyMember>(domainFamilyMembers, pageIndex, pageSize, familyMembers.TotalCount, familyMembers.TotalPages);

            return result;
        }

        public async Task<Models.PagedResult<Models.FamilyMember>> GetAllAsync(int pageSize, int pageIndex, string orderBy)
        {
            Func<IQueryable<FamilyMember>, IIncludableQueryable<FamilyMember, object>> include = source => source.Include(familyMember => familyMember.FamilyMemberType)
                                                                                                                 .Include(familymember => familymember.FamilyMemberTravelType)
                                                                                                                .Include(familyMember => familyMember.EmployeeGrade)
                                                                                                                    .ThenInclude(eg => eg.Employee)
                                                                                                                    .ThenInclude(e => e.School)
                                                                                                                .Include(familyMember => familyMember.EmployeeGrade)
                                                                                                                    .ThenInclude(eg => eg.Grade);

            return await GetAllAsync(_familyMemberRepository, pageSize, pageIndex, orderBy, include);
        }
        public async Task<Models.PagedResult<Models.FamilyMember>> GetAllFamilyMembersByTypeAsync(string familyMemberType, int pageSize, int pageIndex, string orderBy)
        {

            IList<Models.FamilyMember> domainFamilyMembers = new List<Models.FamilyMember>();

            Func<IQueryable<FamilyMember>, IIncludableQueryable<FamilyMember, object>> include = source => source.Include(familyMember => familyMember.FamilyMemberType)
                                                                                                                .Include(familymember => familymember.FamilyMemberTravelType)
                                                                                                                .Include(familyMember => familyMember.EmployeeGrade)
                                                                                                                    .ThenInclude(eg => eg.Employee)
                                                                                                                    .ThenInclude(e => e.School)
                                                                                                                .Include(familyMember => familyMember.EmployeeGrade)
                                                                                                                    .ThenInclude(eg => eg.Grade);

            var familyMembers = await _familyMemberRepository.GetPagedListAsync(include: include, pageIndex: pageIndex, pageSize: pageSize, orderBy: x => x.OrderBy(orderBy), predicate: x => x.FamilyMemberType.Description == familyMemberType);

            familyMembers.Items.ToList().ForEach(fm => domainFamilyMembers.Add(_mapper.Map<Models.FamilyMember>(fm)));

            var result = new Models.PagedResult<Models.FamilyMember>(domainFamilyMembers, pageIndex, pageSize, familyMembers.TotalCount, familyMembers.TotalPages);

            return result;
        }

        public async Task<Models.PagedResult<Models.FamilyMember>> GetAllFamilyMembersByTypeIdAsync(Guid familyMemberTypeId, int pageSize, int pageIndex, string orderBy)
        {

            IList<Models.FamilyMember> domainFamilyMembers = new List<Models.FamilyMember>();

            Func<IQueryable<FamilyMember>, IIncludableQueryable<FamilyMember, object>> include = source => source.Include(familyMember => familyMember.FamilyMemberType)
                                                                                                                .Include(familymember => familymember.FamilyMemberTravelType)
                                                                                                                .Include(familyMember => familyMember.EmployeeGrade)
                                                                                                                    .ThenInclude(eg => eg.Employee)
                                                                                                                    .ThenInclude(e => e.School)
                                                                                                                .Include(familyMember => familyMember.EmployeeGrade)
                                                                                                                    .ThenInclude(eg => eg.Grade);

            var familyMembers = await _familyMemberRepository.GetPagedListAsync(include: include, pageIndex: pageIndex, pageSize: pageSize, orderBy: x => x.OrderBy(orderBy), predicate: x => x.FamilyMemberTypeId == familyMemberTypeId);

            familyMembers.Items.ToList().ForEach(fm => domainFamilyMembers.Add(_mapper.Map<Models.FamilyMember>(fm)));

            var result = new Models.PagedResult<Models.FamilyMember>(domainFamilyMembers, pageIndex, pageSize, familyMembers.TotalCount, familyMembers.TotalPages);

            return result;
        }
        #endregion

        #region Update
        public async Task<Models.FamilyMember> UpdateAsync(Models.FamilyMember domainObject)
        {
            return await UpdateAsync(_familyMemberRepository, domainObject);
        }
        #endregion

        #region Delete
        public async Task DeleteAsync(Guid id)
        {
            await DeleteAsync(id, _familyMemberRepository);
        }
        #endregion
    }
}

