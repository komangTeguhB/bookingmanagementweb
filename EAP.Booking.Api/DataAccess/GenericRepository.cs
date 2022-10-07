using EAP.Booking.Api.DataAccess.Interface;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace EAP.Booking.Api.DataAccess
{
    public abstract class GenericRepository<C, T> :
    IGenericRepository<T> where T : class where C : DbContext, new()
    {

        private C _entities = new C();
        public C Context
        {

            get { return _entities; }
            set { _entities = value; }
        }

        public virtual IQueryable<T> GetAll()
        {
            IQueryable<T> query = _entities.Set<T>();
            return query;
        }

        public virtual IQueryable<T> GetAllWithRelation(params Expression<Func<T, object>>[] includeExpressions)
        {
            IQueryable<T> query = _entities.Set<T>();
            foreach (var includeExpression in includeExpressions)
            {
                query = query.Include(includeExpression);
            }
            return query;
        }

        public IQueryable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {

            IQueryable<T> query = _entities.Set<T>().Where(predicate);
            return query;
        }

        public virtual void Add(T entity)
        {
            _entities.Set<T>().Add(entity);
        }

        public virtual void Delete(T entity)
        {
            _entities.Set<T>().Remove(entity);
        }

        public virtual void Edit(T entity)
        {
            _entities.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public virtual void Save()
        {
            _entities.SaveChanges();
        }
    }
}