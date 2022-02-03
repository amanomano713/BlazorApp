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
        public string? Id { get; set; }
        [Required]
        public string? CodigoId { get; set; }
        [Required]
        public string? CodPackage { get; set; }
        [Required]
        public DateTime? DateCreated { get; set; }
        [Required]
        public float Interes { get; set; }
        [Required]
        public float Porcentaje { get; set; }
    }
}
