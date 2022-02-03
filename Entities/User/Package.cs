
using BlazorApp.DataAcess.Bases;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorApp.Entities.User
{
    [Table("Packages")]
    [Index(nameof(Id), IsUnique = true)]

    public class Packages : Audited
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string? IdAfiliado { get; set; }
        [Required]
        public string? CodPackage { get; set; }
        [Required]
        public float Monto { get; set; }

        
    }
}
