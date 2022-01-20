using BlazorApp.Data.EF;
using BlazorApp.DataAcess.Bases;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.DataAcess.Infraestructure.Repositories
{
    public abstract class SqlRepository<T> : IRepository<T> where T : class
    {
        private readonly Context Context;

        protected readonly DbSet<T> DbSet;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return Context;
            }
        }

        protected SqlRepository(Context context)
        {
            Context = context;
            DbSet = context.Set<T>();
        }

        #region IRepository<T> Members

        public virtual IQueryable<T> GetAll()
        {
            return (IOrderedQueryable<T>)DbSet;
        }

        public abstract Task<T> GetById(string id);

        #endregion
    }
}