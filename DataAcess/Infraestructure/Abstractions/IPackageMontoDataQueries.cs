using BlazorApp.Models;

namespace BlazorApp.DataAcess.Infraestructure.Abstractions
{
    public interface IPackageMontoDataQueries
    {
        public Task<PackagesTotalDTO> TotalPackageDataAsync(string id);

    }
}
