using DocumentLight.Core.Entities;
using DocumentLight.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DocumentLight.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationContext _dbContext;
        protected readonly DbSet<T> DbSet;

        public Repository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
            DbSet = _dbContext.Set<T>();
        }

        public T Add(T obj)
        {
            DbSet.Add(obj);
            return obj;
        }

        public async Task<T> AddAsync(T obj)
        {
            await DbSet.AddAsync(obj);
            return obj;
        }

        public void Delete(Guid id)
        {
            var obj = DbSet.First(x => x.Id == id);
            DbSet.Remove(obj);
        }

        public async Task DeleteAsync(Guid id)
        {
            var obj = await DbSet.FirstAsync(x => x.Id == id);
            DbSet.Remove(obj);
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public IQueryable<T> FindBy<T2>(Expression<Func<T, bool>> predicate, params Expression<Func<T, T2>>[] paths)
        {
            foreach (var path in paths)
            {
                DbSet.Include(path);
            }

            return DbSet.Where(predicate);
        }

        public IQueryable<T> GetAll()
        {
            return DbSet.AsQueryable();
        }

        public T GetById(Guid id)
        {
            return DbSet.FirstOrDefault(x => x.Id == id);
        }

        public T Update(T obj)
        {
            DbSet.Update(obj);
            return obj;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> obj)
        {
            await DbSet.AddRangeAsync(obj);
            return obj;
        }

        public void Delete(T obj)
        {
            DbSet.Remove(obj);
        }

        public void DeleteRange(IEnumerable<T> obj)
        {
            DbSet.RemoveRange(obj);
        }

        public IQueryable<T> GetAllWithIncludies(params Expression<Func<T, object>>[] includeProperties)
        {
            var query = DbSet.AsQueryable();
            foreach (var include in includeProperties)
            {
                query = query.Include(include);
            }

            return query;
        }

        public T GetByIdWithIncludies(Guid id, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = DbSet.Where(x => x.Id == id);
            foreach (var include in includeProperties)
            {
                query = query.Include(include);
            }

            return query.FirstOrDefault();
        }
    }
}
