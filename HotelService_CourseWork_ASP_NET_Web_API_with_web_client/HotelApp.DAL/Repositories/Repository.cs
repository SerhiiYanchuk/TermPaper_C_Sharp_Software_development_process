using HotelApp.DAL.EF;
using HotelApp.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace HotelApp.DAL.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity>
        where TEntity: class
    {
        protected readonly HotelDbContext context;
        public Repository(HotelDbContext context)
        {
            this.context = context;
        }
        public virtual IEnumerable<TEntity> FindAll(bool isTracked = true)
        {
            IQueryable<TEntity> set = context.Set<TEntity>();
            if (!isTracked)
                set.AsNoTracking();
            return set.ToList();
        }
        public virtual TEntity FindById(int id, bool isTracked = true)
        {
            if (!isTracked)
                context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            TEntity item = context.Find<TEntity>(id);
            if (!isTracked)
                context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
            return item;
        }
        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, bool isTracked = true)
        {
            IQueryable<TEntity> set = context.Set<TEntity>().Where(predicate);
            if (!isTracked)
                set.AsNoTracking();
            return set.ToList();
        }
        public virtual void Insert(TEntity item)
        {
            context.Add<TEntity>(item);
            //return item;
        }
        public virtual void Update(TEntity item)
        {
            context.Update<TEntity>(item);
        }
        public virtual void Delete(int id)
        {
            var temp = FindById(id);
            if (temp != null)
                context.Remove<TEntity>(temp);
        }
        public virtual void Delete(TEntity item)
        {
            context.Remove<TEntity>(item);
        }
        public virtual IQueryable<TEntity> GetQuery()
        {
            return context.Set<TEntity>();
        }
        public abstract bool CheckAvailability(int id);
    }
}
