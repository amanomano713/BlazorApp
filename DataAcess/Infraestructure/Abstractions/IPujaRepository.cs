using BlazorApp.DataAcess.Bases;
using BlazorApp.Entities.User;

namespace BlazorApp.DataAcess.Infraestructure.Abstractions
{
    public interface IPujaRepository : IRepository<Puja>
    {
        Puja Add(Puja puja);
    }
}
