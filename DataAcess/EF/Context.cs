using BlazorApp.DataAcess.Bases;
using BlazorApp.DataAcess.EF.Configurations;
using BlazorApp.DataAcess.EF.Extensions;
using BlazorApp.Entities.User;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace BlazorApp.Data.EF
{
    public class Context : DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;

        private readonly Context _context;

        private IDbContextTransaction _currentTransaction;

        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

        public bool HasActiveTransaction => _currentTransaction != null;

        public Context(DbContextOptions<Context> options) : base(options)
        {
            _mediator = this.GetService<IMediator>() ?? throw new InvalidOperationException($"Dependency of type {nameof(IMediator)} was not found.");

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserDataMappings());
            builder.ApplyConfiguration(new PackagesMappings());
            builder.ApplyConfiguration(new TransferMappings());
            builder.ApplyConfiguration(new WithdrawalMappings());
            builder.ApplyConfiguration(new MovPackageMappings());
            base.OnModelCreating(builder);
        }

        public DbSet<UserData> UserData { get; set; }
        public DbSet<Packages> Packages { get; set; }
        public DbSet<Transfer> Transfer { get; set; }
        public DbSet<Withdrawal> Withdrawal { get; set; }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await _mediator.DispatchDomainEventsAsync(this);

            Audit();

            _ = await base.SaveChangesAsync(cancellationToken);

            return true;
        }


        private void Audit()
        {
            foreach (var item in ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added && e.Entity is Audited))
            {
                // New added audited entity must have creation and modification info
                var entity = item.Entity as Audited;
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
                entity.CreatedDate = DateTime.UtcNow;
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            }

            foreach (var item in ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified && e.Entity is Audited))
            {
                // Modified audited entities must update modification info only
                var entity = item.Entity as Audited;
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
                entity.UpdatedDate = DateTime.UtcNow;
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

            }
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null) return null;

            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            return _currentTransaction;
        }

        public Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not the current one.");

            return CommitTransactionInternalAsync(transaction);
        }

        private async Task CommitTransactionInternalAsync(IDbContextTransaction transaction)
        {
            try
            {
                await SaveChangesAsync();
                transaction.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
    }
}
