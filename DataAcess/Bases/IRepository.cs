using System.Linq.Expressions;

namespace BlazorApp.DataAcess.Bases
{
    public interface IRepository<T> where T : class
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
