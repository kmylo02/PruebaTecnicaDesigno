using Domain.Model;
using Repository.Implement;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkUnit.Interface;

namespace WorkUnit.Implement
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext context;
        private readonly Dictionary<Type, object> repositories = new Dictionary<Type, object>();
        private bool disposed = false;

        public UnitOfWork(string connectionString)
        {
            this.context = new DataContext(connectionString);
        }

        public UnitOfWork(DataContext context)
        {
            this.context = context;
        }

        public IRepository<T> Repository<T>() where T : class
        {
            if (this.repositories.ContainsKey(typeof(T)))
            {
                return this.repositories[typeof(T)] as IRepository<T>;
            }

            IRepository<T> repo = new Repository<T>(this.context);
            this.repositories.Add(typeof(T), repo);
            return repo;
        }

        public bool Save()
        {
            bool returnValue = true;
            using (var dbContextTransaction = this.context.Database.BeginTransaction())
            {
                try
                {
                    this.context.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    returnValue = false;
                    dbContextTransaction.Rollback();
                }
            }

            return returnValue;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed && disposing)
            {
                this.context.Dispose();
            }

            this.disposed = true;
        }
    }
}
