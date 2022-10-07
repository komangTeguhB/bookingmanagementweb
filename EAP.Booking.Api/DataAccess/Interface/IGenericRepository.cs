using System;
using System.Linq;
using System.Linq.Expressions;

namespace EAP.Booking.Api.DataAccess.Interface
{
    public interface IGenericRepository<T> where T: class
    {
        IQueryable<T> GetAll();

        IQueryable<T> GetAllWithRelation(params Expression<Func<T, object>>[] includeExpressions);

        IQueryable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate);

        void Add(T entity);

        void Delete(T entity);

        void Edit(T entity);

        void Save();
    }
}