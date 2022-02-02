using BlazorApp.DataAcess.Bases;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorApp.Entities.User
{
    [Table("MovPackage")]
    [Index(nameof(Id), IsUnique = true)]
    public class MovPackage 
    {
        public string? Id { get; set; }
        public string? CodPackage { get; set; }
        public new DateTime? CreatedDate { get; set; }
        public float Interes { get; set; }
        public float Porcentaje { get; set; }
    }
}
