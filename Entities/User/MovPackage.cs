using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorApp.Entities.User
{
    [Table("MovPackage")]
    [Index(nameof(Id), IsUnique = true)]
    public class MovPackage : DataAcess.Bases.Audited
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string? IdAfiliado { get; set; }
        public int IdPackage { get; set; }
        public string? CodPackage { get; set; }
        [Required]
        public DateTime? DateCreated { get; set; }
        public float Interes { get; set; }
        public float Porcentaje { get; set; }
        public float MontoPackage { get; set; }
        public float MontoRetiro { get; set; }
        public float MontoTransferido { get; set; }
        public string? IdAfiliadoDestino { get; set; }
    }
}
