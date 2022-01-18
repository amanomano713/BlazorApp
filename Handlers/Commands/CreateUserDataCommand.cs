using BlazorApp.Entities.User;
using MediatR;
using System.Runtime.Serialization;

namespace BlazorApp.Handlers.Commands
{
    [DataContract]
    public class CreateUserDataCommand : IRequest<UserData>
    {

        [DataMember]
        public string id { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string surname { get; set; }
        [DataMember]
        public DateTime dateOfbirth { get; set; }
        [DataMember]
        public string city { get; set; }
        [DataMember]
        public string zipcode { get; set; }
        [DataMember]
        public string mobile { get; set; }
        [DataMember]
        public string wallet { get; set; }
        [DataMember]
        public string email { get; set; }

    }
}
