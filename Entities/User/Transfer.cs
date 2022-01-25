using BlazorApp.DataAcess.Bases;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorApp.Entities.User
{
    [Table("Transfer")]
    [Index(nameof(Id), IsUnique = false)]

    public class Transfer : Audited
    {
        public string? Id { get; set; }
        public string? Afiliado { get; set; }
        public float Monto { get; set; }

    }
}
