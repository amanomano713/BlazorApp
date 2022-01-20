using BlazorApp.DataAcess.Bases;
using BlazorApp.Entities.User;

namespace BlazorApp.DataAcess.Infraestructure.Abstractions
{
    public interface IUserDataRepository : IRepository<UserData> 
    {

        UserData Add(UserData userdata);

        Task Update(UserData userdata);

    }
}
