using BlazorApp.Entities.User;
using MediatR;
using System.Runtime.Serialization;

namespace BlazorApp.Handlers.Commands
{
    [DataContract]
    public class CreateWithdrawalCommand : IRequest<Withdrawal>
    {
        [DataMember]
        public string? Id { get; set; }
        [DataMember]
        public string? Wallet { get; set; }
        [DataMember]
        public float Monto { get; set; }
    }
}
