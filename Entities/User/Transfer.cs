using BlazorApp.DataAcess.Bases;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorApp.Entities.User
{
    [Table("Transfer")]
    [Index(nameof(Id), IsUnique = false)]

    public class Transfer : Audited
    {
        public string? Id { get; set; }
        [Required]
        public string? Afiliado { get; set; }
        [Required]
        public float Monto { get; set; }

    }
}
