using BlazorApp.DataAcess.Bases;
using BlazorApp.Entities.User;

namespace BlazorApp.DataAcess.Infraestructure.Abstractions
{
    public interface ITransferRepository : IRepository<Transfer>
    {
        Transfer Add(Transfer transfer);
    }
}
