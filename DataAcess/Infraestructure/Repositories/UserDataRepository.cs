using BlazorApp.Data.EF;
using BlazorApp.DataAcess.Bases;
using BlazorApp.DataAcess.Infraestructure.Abstractions;
using BlazorApp.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.DataAcess.Infraestructure.Repositories
{
    public class UserDataRepository :  IUserDataRepository
    {
        private readonly Context _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }


        public UserDataRepository(Context context) {
                _context = context ?? throw new ArgumentNullException(nameof(context));   
        }

        public async Task<UserData> GetAsync(String id) => await _context.UserData.AsNoTracking().FirstOrDefaultAsync(item => item.Id == id);

        public UserData Add(UserData userdata)
        {
            userdata.CreatedDate = DateTime.Now;

            return  _context.UserData.Add(userdata).Entity;
        }


        public async Task<UserData> Update(UserData item)
        {

            var changedEntriesCopy = _context.ChangeTracker.Entries()
                    .Where(e => e.State == EntityState.Added ||
                                e.State == EntityState.Modified ||
                                e.State == EntityState.Deleted)
                    .ToList();

            foreach (var entry in changedEntriesCopy)
                entry.State = EntityState.Detached;

            _context.Entry(item).State = EntityState.Modified;

            var result = _context.UserData.Update(item).Entity;

            return result;
        }

    }
}
