using BlazorApp.Data.EF;
using BlazorApp.DataAcess.Bases;
using BlazorApp.DataAcess.Infraestructure.Abstractions;
using BlazorApp.Entities.User;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

        public async Task<UserData> GetAsync(String id) => await _context.UserData.FirstOrDefaultAsync(item => item.Id == id);

        public void Add(UserData entity)
        {
            throw new NotImplementedException();
        }

        public void Update(UserData entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<UserData> IQueryableList(Expression<Func<UserData, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public List<UserData> Get(Expression<Func<UserData, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<UserData> AddAsync(UserData entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(UserData entity)
        {
            throw new NotImplementedException();
        }
    }
}
