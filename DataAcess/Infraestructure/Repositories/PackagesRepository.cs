using BlazorApp.Data.EF;
using BlazorApp.DataAcess.Bases;
using BlazorApp.DataAcess.Infraestructure.Abstractions;
using BlazorApp.Entities.User;

namespace BlazorApp.DataAcess.Infraestructure.Repositories
{
    public class PackagesRepository : SqlRepository<Packages>, IPackagesRepository
    {
        public new IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        private readonly Context _context;
        public PackagesRepository(Context context)
            : base(context)
        {
            _context = context;
        }
        public Packages Add(Packages item)
        {
            return _context.Packages.Add(item).Entity;
        }

        public override Task<Packages> GetById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
