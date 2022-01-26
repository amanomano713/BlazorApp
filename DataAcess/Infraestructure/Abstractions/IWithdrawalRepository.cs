using BlazorApp.DataAcess.Bases;
using BlazorApp.Entities.User;

namespace BlazorApp.DataAcess.Infraestructure.Abstractions
{
    public interface IWithdrawalRepository : IRepository<Withdrawal>
    {
        Withdrawal Add(Withdrawal withdrawal);
    }
}
