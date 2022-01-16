using Domain.Entities.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistence.EF
{
    public class Context : DbContext, IUnitOfWork
    {

        public Context(DbContextOptions<Context> options) : base(options)
        {
            //_mediator = this.GetService<IMediator>() ?? throw new InvalidOperationException($"Dependency of type {nameof(IMediator)} was not found.");
            //_httpContextAccessor = this.GetService<IHttpContextAccessor>() ?? throw new InvalidOperationException($"Dependency of type {nameof(IHttpContextAccessor)} was not found.");
        }

        public Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
