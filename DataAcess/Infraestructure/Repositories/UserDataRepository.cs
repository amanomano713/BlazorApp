using BlazorApp.Data.EF;
using BlazorApp.DataAcess.Bases;
using BlazorApp.DataAcess.Infraestructure.Abstractions;
using BlazorApp.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.DataAcess.Infraestructure.Repositories
{
    public class UserDataRepository : SqlRepository<UserData>, IUserDataRepository
    {

        public new IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        private readonly Context _context;
        public UserDataRepository(Context context)
            : base(context)
        {
            _context = context;
        }

#pragma warning disable CS0114 // El miembro oculta el miembro heredado. Falta una contraseña de invalidación
        public  IQueryable<UserData> GetAll()
#pragma warning restore CS0114 // El miembro oculta el miembro heredado. Falta una contraseña de invalidación
        {
            return DbSet;
        }
        public UserData Add(UserData item)
        {
            return  _context.UserData.Add(item).Entity;
        }

        public async Task Update(UserData item)
        {

             var entry = _context.Entry(item);
            if (entry.State == EntityState.Detached)
            {
                var attachedOrder = await GetById(item.Id);
                if (attachedOrder != null)
                {
                    _context.Entry(attachedOrder).CurrentValues.SetValues(item);
                }
                else
                {
                    entry.State = EntityState.Modified;
                }
            }
        }

        public override async Task<UserData> GetById(string id)
        {
#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return await GetAll().SingleOrDefaultAsync(c => c.Id == id);
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }
    }
}
