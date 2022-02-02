using BlazorApp.Entities.User;

namespace BlazorApp.DataAcess.Infraestructure.Abstractions
{
    public interface IMovPackageDataQueries
    {
        public Task<IEnumerable<MovPackage>> GetAfiliadoDataAsync(string id);

    }
}
