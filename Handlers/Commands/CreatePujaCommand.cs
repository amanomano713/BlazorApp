using MediatR;
using System.Runtime.Serialization;
using BlazorApp.Entities.User;
namespace BlazorApp.Handlers.Commands
{
    [DataContract]
    public class CreatePujaCommand : IRequest<Puja>
    {
        [DataMember]
        public string? IdAfiliado { get; set; }
        [DataMember]
        public string? IdPuja { get; set; }
        [DataMember]
        public float Monto { get; set; }
    }
}
