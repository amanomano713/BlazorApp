using BlazorApp.DataAcess.Bases;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorApp.Entities.User
{
    [Table("Withdrawal")]
    [Index(nameof(Id), IsUnique = false)]

    public class Withdrawal : Audited
    {
        public string? Id { get; set; }
        public string? Wallet { get; set; }
        public float Monto { get; set; }

    }
}
