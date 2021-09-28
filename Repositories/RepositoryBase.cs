using Contracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        public AppDBContext AppDBContext { get; set; }

        public RepositoryBase(AppDBContext appDBContext)
        {
            this.AppDBContext = appDBContext;
        }

        public int Count()
        {
            return AppDBContext.Set<T>().Count();
        }

        public void Create(T entity)
        {
            this.AppDBContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            this.AppDBContext.Set<T>().Remove(entity);
        }

        public void DeleteAll(IEnumerable<T> entities)
        {
            this.AppDBContext.Set<T>().RemoveRange(entities);
        }

        public IQueryable<T> FindAll()
        {
            return this.AppDBContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.AppDBContext.Set<T>().Where(expression).AsNoTracking();
        }

        public IQueryable<T> FindBySpecialCondition(Expression<Func<T, bool>> expression)
        {
            return this.AppDBContext.Set<T>().Where(expression).AsNoTracking();
        }

        public void Update(T entity)
        {
            this.AppDBContext.Set<T>().Update(entity);
        }        
    }
}
