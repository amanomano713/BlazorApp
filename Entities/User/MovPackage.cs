using BlazorApp.DataAcess.Bases;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorApp.Entities.User
{
    [Table("MovPackage")]
    [Index(nameof(Id), IsUnique = true)]
    public class MovPackage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string? IdAfiliado { get; set; }
        [Required]
        public int IdPackage { get; set; }
        [Required]
        public string? CodPackage { get; set; }
        [Required]
        public DateTime? DateCreated { get; set; }
        [Required]
        public float Interes { get; set; }
        [Required]
        public float Porcentaje { get; set; }
        [Required]
        public float Monto { get; set; }
    }
}
