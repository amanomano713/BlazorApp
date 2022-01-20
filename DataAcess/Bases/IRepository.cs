using System.Linq.Expressions;

namespace BlazorApp.DataAcess.Bases
{
    public interface IRepository<T> 
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
