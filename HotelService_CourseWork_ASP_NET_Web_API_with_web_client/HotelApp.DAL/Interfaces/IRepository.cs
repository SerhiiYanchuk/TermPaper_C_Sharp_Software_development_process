using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace HotelApp.DAL.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> FindAll(bool isTracked = true);
        TEntity FindById(int id, bool isTracked = true);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, bool isTracked = true);
        void Insert(TEntity item);
        void Update(TEntity item);
        void Delete(int id);
        void Delete(TEntity item);
        IQueryable<TEntity> GetQuery();
        bool CheckAvailability(int id);
    }
}
