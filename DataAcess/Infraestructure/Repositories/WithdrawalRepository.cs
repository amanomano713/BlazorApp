using BlazorApp.Data.EF;
using BlazorApp.DataAcess.Bases;
using BlazorApp.DataAcess.Infraestructure.Abstractions;
using BlazorApp.Entities.User;

namespace BlazorApp.DataAcess.Infraestructure.Repositories
{
    public class WithdrawalRepository : SqlRepository<Withdrawal>, IWithdrawalRepository
    {
        public new IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        private readonly Context _context;
        public WithdrawalRepository(Context context)
            : base(context)
        {
            _context = context;
        }
        public Withdrawal Add(Withdrawal item)
        {
            return _context.Withdrawal.Add(item).Entity;
        }

        public override Task<Withdrawal> GetById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
