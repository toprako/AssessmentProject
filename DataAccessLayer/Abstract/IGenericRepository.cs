using EntityLayer.Concrete.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        IQueryable<T> FindAll();
        IQueryable<T> FindByFilter(Expression<Func<T, bool>> expression);
        T? GetById(Guid Id);
        string Insert(T Entity);
        string Delete(T Entity);
        string Update(T Entity);
    }
}
