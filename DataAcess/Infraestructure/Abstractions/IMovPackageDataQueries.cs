using BlazorApp.Entities.User;

namespace BlazorApp.DataAcess.Infraestructure.Abstractions
{
    public interface IMovPackageDataQueries
    {
        public Task<IEnumerable<MovPackageDTO>> GetAfiliadoDataAsync(string id);

    }
}
