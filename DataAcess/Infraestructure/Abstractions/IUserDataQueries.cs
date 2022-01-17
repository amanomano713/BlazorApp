using BlazorApp.Entities.User;

namespace BlazorApp.DataAcess.Infraestructure.Abstractions
{
    public interface IUserDataQueries
    {
        public Task<UserData> GetUserDataAsync(String email);

    }
}
