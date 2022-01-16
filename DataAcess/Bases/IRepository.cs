using System.Linq.Expressions;

namespace BlazorApp.DataAcess.Bases
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        IQueryable<T> IQueryableList(Expression<Func<T, bool>> expression);
        List<T> Get(Expression<Func<T, bool>> expression);
        Task<T> AddAsync(T entity);
        void Delete(T entity);
    }
}
