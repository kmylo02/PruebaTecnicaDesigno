using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implement
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        internal DataContext context;
        internal DbSet<TEntity> dbSet;

        public Repository(DataContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
             Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = this.dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual TEntity FindSingleBy(
         Expression<Func<TEntity, bool>> filter = null)
        {
            TEntity query = null;
            if (filter != null)
            {
                query = this.dbSet.Where(filter).SingleOrDefault();
            }

            return query;
        }

        public virtual IEnumerable<TEntity> GetAllBy(
         Expression<Func<TEntity, bool>> filter = null)
        {
            IEnumerable<TEntity> query = null;
            if (filter != null)
            {
                query = dbSet.Where(filter);
            }

            return query;
        }

        public virtual TEntity GetByID(object id)
        {
            return this.dbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            this.dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = this.dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (this.context.Entry(entityToDelete).State == EntityState.Detached)
            {
                this.dbSet.Attach(entityToDelete);
            }

            this.dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            this.dbSet.Update(entityToUpdate);
            this.context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual void Update(object id, TEntity entityToUpdate)
        {
            TEntity entity = this.dbSet.Find(id);
            this.context.Entry(entity).CurrentValues.SetValues(entityToUpdate);
            Update(entity);
        }
    }
}
