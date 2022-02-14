using BlazorApp.Data.EF;
using BlazorApp.DataAcess.Bases;
using BlazorApp.DataAcess.Infraestructure.Abstractions;
using BlazorApp.Entities.User;

namespace BlazorApp.DataAcess.Infraestructure.Repositories
{
    public class PujaRepository : SqlRepository<Puja>, IPujaRepository
    {
        public new IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        private readonly Context _context;
        public PujaRepository(Context context)
            : base(context)
        {
            _context = context;
        }
        public Puja Add(Puja item)
        {
            return _context.Puja.Add(item).Entity;
        }

        public override Task<Puja> GetById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
