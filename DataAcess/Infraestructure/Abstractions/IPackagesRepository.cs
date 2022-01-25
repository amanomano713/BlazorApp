using BlazorApp.DataAcess.Bases;
using BlazorApp.Entities.User;

namespace BlazorApp.DataAcess.Infraestructure.Abstractions
{
    public interface IPackagesRepository : IRepository<Packages>
    {
        Packages Add(Packages packages);
    }
}
