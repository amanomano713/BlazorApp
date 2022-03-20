using BlazorApp.Entities.User;
using MediatR;
using System.Runtime.Serialization;

namespace BlazorApp.Handlers.Commands
{
    [DataContract]
    public class CreateTransferCommand : IRequest<Transfer>
    {
        [DataMember]
        public string? Id { get; set; }
        [DataMember]
        public string? Afiliado { get; set; }
        [DataMember]
        public float Monto { get; set; }

        [DataMember]
        public string? AfiliadoDestino { get; set; }
    }
}
