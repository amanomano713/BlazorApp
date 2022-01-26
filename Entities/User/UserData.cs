using BlazorApp.DataAcess.Bases;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorApp.Entities.User
{
    [Table("UserData")]
    [Index(nameof(Id), IsUnique = true)]
 
    public class UserData : Audited
    {
        public string Id { get; set; }
        public string? name { get; set; }
        public string? surname { get; set; }
        public DateTime dateOfbirth { get; set; }
        public string? city { get; set; }
        public string? mobile { get; set; }
        public string? wallet { get; set; }
        public string? email { get; set; }

    }
}
