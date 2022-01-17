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


        public void Update(UserData userdata)
        {
            userdata.UpdatedDate = DateTime.Now;

            _context.Entry(userdata).State = EntityState.Modified;
        }

    }
}
