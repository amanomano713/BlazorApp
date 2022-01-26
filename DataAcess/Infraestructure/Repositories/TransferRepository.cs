using BlazorApp.Data.EF;
using BlazorApp.DataAcess.Bases;
using BlazorApp.DataAcess.Infraestructure.Abstractions;
using BlazorApp.Entities.User;

namespace BlazorApp.DataAcess.Infraestructure.Repositories
{
    public class TransferRepository : SqlRepository<Transfer>, ITransferRepository
    {
        public new IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        private readonly Context _context;
        public TransferRepository(Context context)
            : base(context)
        {
            _context = context;
        }
        public Transfer Add(Transfer item)
        {
            return _context.Transfer.Add(item).Entity;
        }

        public override Task<Transfer> GetById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
