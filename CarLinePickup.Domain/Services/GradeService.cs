using System.Threading.Tasks;
using System;
using AutoMapper;
using CarLinePickup.Domain.Services.Interfaces;
using Microsoft.Extensions.Options;
using CarLinePickup.Data.Models;
using CarLinePickup.Options.DomainOptions;
using Arch.EntityFrameworkCore.UnitOfWork;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using CarLinePickup.Domain.Models.Interfaces;
using System.Linq;
using CarLinePickup.Domain.Extensions;
using Microsoft.AspNetCore.JsonPatch;

namespace CarLinePickup.Domain.Services
{
    public class GradeService : ServiceBase<Grade, Models.Grade>, IGradeService<Models.Grade>
    {
        private readonly IRepository<Grade> _gradeRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly DomainOptions _options;

        public GradeService(IOptions<DomainOptions> options, IMapper mapper,
            IUnitOfWork unitOfWork) : base(unitOfWork, mapper)
        {
            _gradeRepository = unitOfWork.GetRepository<Grade>();
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _options = options.Value;
        }

        #region Create
        public async Task<Models.Grade> CreateAsync(Guid schoolId, Models.Grade grade)
        {
            Grade dataGrade = new Grade();

            dataGrade = _mapper.Map<Grade>(grade);

            dataGrade.CreatedDate = DateTime.Now;
            dataGrade.ModifiedDate = DateTime.Now;

            dataGrade.SchoolId = schoolId;

            dataGrade.School = null;
            
            await _gradeRepository.InsertAsync(dataGrade);

            await _unitOfWork.SaveChangesAsync();

            return await GetAsync(dataGrade.Id);
        }
        #endregion

        #region Read
        public async Task<Models.Grade> GetAsync(Guid id)
        {
            return await GetAsync(id, _gradeRepository);
        }

        public async Task<Models.PagedResult<Models.Grade>> GetAllAsync(int pageSize, int pageIndex, string orderBy)
        {
            return await GetAllAsync(_gradeRepository, pageSize, pageIndex, orderBy);
        }

        public async Task<Models.PagedResult<Models.Grade>> GetAllGradesBySchoolIdAsync(Guid schoolId, int pageSize, int pageIndex, string orderBy)
        {
            IList<Models.Grade> domainGrades = new List<Models.Grade>();

            var grades = await _gradeRepository.GetPagedListAsync(predicate: x => x.SchoolId == schoolId, pageIndex: pageIndex, pageSize: pageSize, orderBy: x => x.OrderBy(orderBy));

            grades.Items.ToList().ForEach(grade => domainGrades.Add(_mapper.Map<Models.Grade>(grade)));

            var result = new Models.PagedResult<Models.Grade>(domainGrades, pageIndex, pageSize, grades.TotalCount, grades.TotalPages);

            return result;
        }
        #endregion

        #region Update
        public async Task<Models.Grade> UpdateAsync(Models.Grade domainObject)
        {
            return await UpdateAsync(_gradeRepository, domainObject);
        }
        #endregion

        #region Delete
        public async Task DeleteAsync(Guid id)
        {
            await DeleteAsync(id, _gradeRepository);
        }
        #endregion
    }
}

