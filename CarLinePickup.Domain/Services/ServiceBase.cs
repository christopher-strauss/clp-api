using Arch.EntityFrameworkCore.UnitOfWork;
using CarLinePickup.Domain.Services.Interfaces;
using CarLinePickup.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using CarLinePickup.Domain.Extensions;
using AutoMapper;
using CarLinePickup.Domain.Models.Interfaces;
using Microsoft.EntityFrameworkCore.Query;
using CarLinePickup.Domain.Models;

namespace CarLinePickup.Domain.Services
{
    public abstract class ServiceBase<T,S> where T : class, IEntity where S : class, IDomainModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        protected ServiceBase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task DeleteAsync(Guid id, IRepository<T> repository)
        {
            var dataObject = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id == id) ??
                  throw new KeyNotFoundException($"There is no {nameof(T)} with Id: {id}");

            repository.Delete(dataObject);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<S> UpdateAsync(IRepository<T> repository, S domainObject, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            var dataObject = await repository.GetFirstOrDefaultAsync(include: include, predicate: x => x.Id == domainObject.Id) ??
                  throw new KeyNotFoundException($"There is no {nameof(T)} with Id: {domainObject.Id}");

            var updatedDataObject = _mapper.Map(domainObject, dataObject);

            dataObject.ModifiedDate = DateTime.Now;

            repository.Update(dataObject);

            await _unitOfWork.SaveChangesAsync();
            
            return _mapper.Map<S>(dataObject);
        }

        public async Task<PagedResult<S>> GetAllAsync(IRepository<T> repository, int pageSize, int pageIndex, string orderBy, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IList<S> domainObjects = new List<S>();

            var dataObjects = await repository.GetPagedListAsync(include: include, predicate: x => x.Deleted == false, pageIndex: pageIndex, pageSize: pageSize, orderBy: x => x.OrderBy(orderBy));

            dataObjects.Items.ToList().ForEach(o => domainObjects.Add(_mapper.Map<S>(o)));

            var result = new PagedResult<S>(domainObjects, pageIndex, pageSize, dataObjects.TotalCount, dataObjects.TotalPages);

            return result;
        }

        public async Task<S> GetAsync(Guid id, IRepository<T> repository, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            var dataObject = await repository.GetFirstOrDefaultAsync(include: include, predicate: x => x.Id == id);
            return _mapper.Map<S>(dataObject);
        }
    }
}
