using BlazorApp.Entities.User;
using MediatR;
using System.Runtime.Serialization;

namespace BlazorApp.Handlers.Commands
{
    [DataContract]
    public class CreatePackagesCommand : IRequest<Packages>
    {
        [DataMember]
        public string? IdAfiliado{ get; set; }
        [DataMember]
        public string? CodPackage { get; set; }
        [DataMember]
        public float Monto { get; set; }
    }
}
