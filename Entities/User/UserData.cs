using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorApp.Entities.User
{
    [Table("UserData")]
    [Index(nameof(Id), IsUnique = true)]
    public class UserData
    {
        public String Id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
    }
}
