using BlazorApp.DataAcess.Bases;
using BlazorApp.Entities.User;

namespace BlazorApp.DataAcess.Infraestructure.Abstractions
{
    public interface IUserDataRepository : IRepository<UserData> 
    {

        Task<UserData> GetAsync(String id);

        Task<UserData> Add(UserData userdata);
    }
}
