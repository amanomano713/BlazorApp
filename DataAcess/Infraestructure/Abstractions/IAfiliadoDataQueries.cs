using BlazorApp.Entities.User;

namespace BlazorApp.DataAcess.Infraestructure.Abstractions
{
    public interface IAfiliadoDataQueries
    {
        public Task<UserData> GetAfiliadoDataAsync(string afiliado);

    }
}
