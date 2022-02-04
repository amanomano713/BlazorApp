using BlazorApp.DataAcess.Bases;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorApp.Entities.User
{
    [Table("Withdrawal")]
    [Index(nameof(Id), IsUnique = false)]

    public class Withdrawal : Audited
    {
        public string? Id { get; set; }
        [Required]
        public string? Wallet { get; set; }
        [Required]
        public float Monto { get; set; }

    }
}
