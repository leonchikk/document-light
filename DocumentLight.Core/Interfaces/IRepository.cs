using DocumentLight.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DocumentLight.Core.Interfaces
{
    public interface IRepository<T> where T: BaseEntity
    {
        T Add(T obj);
        T Update(T obj);
        void Delete(Guid id);

        Task<T> AddAsync(T obj);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> obj);
        Task DeleteAsync(Guid id);
        void Delete(T obj);
        void DeleteRange(IEnumerable<T> obj);

        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        IQueryable<T> FindBy<T2>(Expression<Func<T, bool>> predicate, params Expression<Func<T, T2>>[] paths);
        IQueryable<T> GetAll();
        IQueryable<T> GetAllWithIncludies(params Expression<Func<T, object>>[] includeProperties);

        T GetById(Guid id);
        T GetByIdWithIncludies(Guid id, params Expression<Func<T, object>>[] includeProperties);
    }
}
