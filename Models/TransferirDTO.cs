using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models
{
    public class TransferirDTO
    {
        public string? Saldo { get; set; }
        public string? Wallet { get; set; }
        [Required(ErrorMessage = "Monto a transferir es Obligatorio")]
        public string? Transferir { get; set; }
        [Required(ErrorMessage = "Código del Afiliado es Obligatorio")]
        public string? Afiliado { get; set; }
        public string? Id { get; set; }

    }
}
