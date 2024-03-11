using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkUnit.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        public IRepository<TEntity> Repository<TEntity>() where TEntity : class;
        public bool Save();
    }
}
