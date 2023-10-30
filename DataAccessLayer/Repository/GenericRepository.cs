using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        public RepositoryContext _context;

        public GenericRepository(RepositoryContext context)
        {
            _context = context;
        }

        public string Delete(T Entity)
        {
            try
            {
                _context.Set<T>().Remove(Entity);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return string.Empty;
        }

        public IQueryable<T> FindAll()
        {
            return _context.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByFilter(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression).AsNoTracking();
        }

        public T? GetById(Guid Id)
        {
            return _context.Set<T>().Find(Id);
        }

        public string Insert(T Entity)
        {
            try
            {
                _context.Set<T>().Add(Entity);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return string.Empty;
        }

        public string Update(T Entity)
        {
            try
            {
                _context.Set<T>().Update(Entity);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return string.Empty;
        }
    }
}
