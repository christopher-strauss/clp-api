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
    public class EmployeeService : ServiceBase<Employee, Models.Employee>, IEmployeeService<Models.Employee>
    {
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IRepository<EmployeeType> _employeeTypeRepository;
        private readonly IRepository<EmployeeGrade> _employeeGradeRepository;
        private readonly IRepository<Grade> _gradeRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly DomainOptions _options;

        public EmployeeService(IOptions<DomainOptions> options, IMapper mapper,
            IUnitOfWork unitOfWork) : base(unitOfWork, mapper)
        {
            _employeeRepository = unitOfWork.GetRepository<Employee>();
            _employeeTypeRepository = unitOfWork.GetRepository<EmployeeType>();
            _employeeGradeRepository = unitOfWork.GetRepository<EmployeeGrade>();
            _gradeRepository = unitOfWork.GetRepository<Grade>();
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _options = options.Value;
        }

        #region Create
        public async Task<Models.Employee> CreateAsync(Guid schoolId, Models.Employee employee)
        {
            var dataEmployee = new Employee();

            dataEmployee = _mapper.Map<Employee>(employee);

            dataEmployee.CreatedDate = DateTime.Now;
            dataEmployee.ModifiedDate = DateTime.Now;

            var dataEmployeeType = await _employeeTypeRepository.GetFirstOrDefaultAsync(predicate: x => x.Id == employee.EmployeeTypeId) ??
                throw new KeyNotFoundException($"There is no {nameof(EmployeeType)} with id: { employee.EmployeeTypeId }");

            dataEmployee.SchoolId = schoolId;

            dataEmployee.EmployeeTypeId = dataEmployeeType.Id;

            dataEmployee.School = null;
            dataEmployee.EmployeeType = null;

            foreach (var grade in employee.Grades)
            {

                var dataGrade = await _gradeRepository.GetFirstOrDefaultAsync(predicate: x => x.Id == grade.Id) ??
                    throw new KeyNotFoundException($"There is no {nameof(Grade)} with grade id : { grade.Id }");


                var dataEmployeeGrade = new EmployeeGrade();

                dataEmployeeGrade.GradeId = grade.Id;

                dataEmployee.EmployeeGrades.Add(dataEmployeeGrade);

            }

            //insert the employee
            await _employeeRepository.InsertAsync(dataEmployee);

            await _unitOfWork.SaveChangesAsync();

            return await GetAsync(dataEmployee.Id);
        }
        #endregion

        #region Read
        public async Task<Models.Employee> GetAsync(Guid id)
        {
            Func<IQueryable<Employee>, IIncludableQueryable<Employee, object>> include = source => source.Include(employee => employee.School)
                                                                                                         .Include(employee => employee.EmployeeType)
                                                                                                         .Include(employee => employee.EmployeeGrades)
                                                                                                            .ThenInclude(employee => employee.Grade);

            return await GetAsync(id, _employeeRepository, include);
        }

        public async Task<Models.Employee> GetByEmailAddressAsync(string emailAddress)
        {
            Func<IQueryable<Employee>, IIncludableQueryable<Employee, object>> include = source => source.Include(employee => employee.School)
                                                                                                         .Include(employee => employee.EmployeeType)
                                                                                                         .Include(employee => employee.EmployeeGrades)
                                                                                                            .ThenInclude(employee => employee.Grade);

            var employee = await _employeeRepository.GetFirstOrDefaultAsync(include: include, predicate: x => x.Email == emailAddress);

            return _mapper.Map<Models.Employee>(employee);
        }

        public async Task<Models.PagedResult<Models.Employee>> GetEmployeesBySchoolId(Guid schoolId, int pageSize, int pageIndex, string orderBy)
        {
            IList<Models.Employee> domainEmployees = new List<Models.Employee>();

            Func<IQueryable<Employee>, IIncludableQueryable<Employee, object>> include = source => source.Include(employee => employee.EmployeeType)
                                                                                                         .Include(employee => employee.EmployeeGrades)
                                                                                                            .ThenInclude(employee => employee.Grade);


            var employees = await _employeeRepository.GetPagedListAsync(include: include, pageIndex: pageIndex, pageSize: pageSize, orderBy: x => x.OrderBy(orderBy), predicate: x => x.SchoolId == schoolId);


            employees.Items.ToList().ForEach(e => domainEmployees.Add(_mapper.Map<Models.Employee>(e)));

            var result = new Models.PagedResult<Models.Employee>(domainEmployees, pageIndex, pageSize, employees.TotalCount, employees.TotalPages);

            return result;
        }

        public async Task<Models.PagedResult<Models.Employee>> GetAllAsync(int pageSize, int pageIndex, string orderBy)
        {
            Func<IQueryable<Employee>, IIncludableQueryable<Employee, object>> include = source => source.Include(employee => employee.School)
                                                                                                         .Include(employee => employee.EmployeeType)
                                                                                                         .Include(employee => employee.EmployeeGrades)
                                                                                                            .ThenInclude(employee => employee.Grade);


            return await GetAllAsync(_employeeRepository, pageSize, pageIndex, orderBy, include);
        }
        #endregion

        #region Update
        public async Task<Models.Employee> UpdateStatus(Models.Employee domainObject, string status)
        {

            Func<IQueryable<Employee>, IIncludableQueryable<Employee, object>> include = source => source.Include(employee => employee.EmployeeGrades);

            var dataObject = await _employeeRepository.GetFirstOrDefaultAsync(include: include, predicate: x => x.Id == domainObject.Id) ??
                throw new KeyNotFoundException($"There is no {nameof(Employee)} with Id: {domainObject.Id}");


            var statusResult = string.Equals(status, "enabled", StringComparison.OrdinalIgnoreCase) ? true : false;

            var updatedDataObject = dataObject.Enabled = statusResult;

            dataObject.ModifiedDate = DateTime.Now;

            _employeeRepository.Update(dataObject);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<Models.Employee>(dataObject);
        }

        public async Task<Models.Employee> UpdateAsync(Models.Employee domainObject)
        {
            Func<IQueryable<Employee>, IIncludableQueryable<Employee, object>> include = source => source.Include(employee => employee.EmployeeGrades);

            var dataObject = await _employeeRepository.GetFirstOrDefaultAsync(include: include, predicate: x => x.Id == domainObject.Id) ??
                throw new KeyNotFoundException($"There is no {nameof(Employee)} with Id: {domainObject.Id}");

            var updatedDataObject = _mapper.Map(domainObject, dataObject);

            dataObject.ModifiedDate = DateTime.Now;

            //delete all the employee grade records if new ones are passed
            if (domainObject.Grades.Count > 0)
            {
                foreach (var grade in dataObject.EmployeeGrades)
                {
                    _employeeGradeRepository.Delete(grade);
                }

            }

            foreach (var grade in domainObject.Grades)
            {

                var dataGrade = await _gradeRepository.GetFirstOrDefaultAsync(predicate: x => x.Id == grade.Id) ??
                    throw new KeyNotFoundException($"There is no {nameof(Grade)} with grade id : { grade.Id }");


                var dataEmployeeGrade = new EmployeeGrade();

                dataEmployeeGrade.GradeId = grade.Id;

                dataObject.EmployeeGrades.Add(dataEmployeeGrade);

            }

            _employeeRepository.Update(dataObject);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<Models.Employee>(dataObject);
        }
        #endregion

        #region Delete
        public async Task DeleteAsync(Guid id)
        {
            await DeleteAsync(id, _employeeRepository);
        }
        #endregion
    }
}

