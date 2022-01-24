using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models
{
    public class RetiroDTO
    {
        [Required(ErrorMessage = "Monto a retirar es Obligatorio")]
        public string? Retiro { get; set; }

        public string? Wallet { get; set; }

        public string? Id { get; set; }

        public int Saldo { get; set; }
    }
}
