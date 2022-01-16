using BlazorApp.Data.EF;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.DataAcess.Bases
{
    public class DbFactory : IDisposable
    {
        private bool _disposed;
        private Func<Context> _connectionFactory;
        private DbContext _dbContext;
        public DbContext DbContext => _dbContext ?? (_dbContext = _connectionFactory.Invoke());

        public DbFactory(Func<Context> dbContextFactory)
        {
            _connectionFactory = dbContextFactory;
        }

        public void Dispose()
        {
            if (!_disposed && _dbContext != null)
            {
                _disposed = true;
                _dbContext.DisposeAsync();
            }
        }

    }
}
