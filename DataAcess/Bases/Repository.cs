using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BlazorApp.DataAcess.Bases
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbFactory _dbFactory;

        private DbSet<T> _dbSet;


        protected DbSet<T> DbSet
        {
            get => _dbSet ?? (_dbSet = _dbFactory.DbContext.Set<T>());
        }

        public Repository(DbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<T> AddAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }

            try
            {
            //    if (typeof(IAuditEntity).IsAssignableFrom(typeof(T)))
            //    {
            //        ((IAuditEntity)entity).CreatedDate = DateTime.Now;
            //    }
                await DbSet.AddAsync(entity);

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be saved: {ex.Message}");
            }
        }


        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(Delete)} entity must not be null");
            }
            DbSet.Remove(entity);
        }

        public void DeleteWhere(Expression<Func<T, bool>> filter = null)
        {
            if (filter == null)
            {
                throw new ArgumentNullException($"{nameof(DeleteWhere)} filter must not be null");
            }

            IQueryable<T> query = DbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            DbSet.RemoveRange(query);
        }

        public IQueryable<T> IQueryableList(Expression<Func<T, bool>> expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException($"{nameof(IQueryableList)} entity must not be null");
            }
            return DbSet.Where(expression);
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(Update)} entity must not be null");
            }
            //if (typeof(IAuditEntity).IsAssignableFrom(typeof(T)))
            //{
            //    ((IAuditEntity)entity).UpdatedDate = DateTime.Now;
            //}
            DbSet.Update(entity);
        }

        public List<T> Get(Expression<Func<T, bool>> expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException($"{nameof(Get)} entity must not be null");
            }

            return DbSet.AsNoTracking().Where(expression).ToList();
        }

        public List<T> List(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException($"{nameof(List)} entity must not be null");
            }

            return DbSet.Where(predicate).ToList();
        }


        public Task<List<T>> ListAsync(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException($"{nameof(List)} entity must not be null");
            }

            return DbSet.Where(predicate).ToListAsync();
        }

        public void Add(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(Add)} entity must not be null");
            }
            //if (typeof(IAuditEntity).IsAssignableFrom(typeof(T)))
            //{
            //    ((IAuditEntity)entity).CreatedDate = DateTime.Now;
            //}
            DbSet.Add(entity);
        }
    }
}
