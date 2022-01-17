using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models
{
    public class UserDTO
    {
        [Required(ErrorMessage = "Nombre es Obligatorio")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Nombre no puede tener menos de 6 caracteres y más de 20 caracteres de longitud")]
        public string name { get; set; }
        [Required(ErrorMessage = "Apellido es Obligatorio")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Apellido no puede tener menos de 6 caracteres y más de 20 caracteres de longitud")]
        public string surname { get; set; }
        [Required(ErrorMessage = "Fecha de Nacimiento es Obligatorio")]
        public DateTime DateOfBirth { get; set; }
        public string city { get; set; }
        public string zipcode { get; set; }

        [Required(ErrorMessage = "Mobile es Obligatorio")]
        public string mobile { get; set; }

        [Required(ErrorMessage = "Wallet BTC es Obligatorio")]
        public string wallet { get; set; }

    }
}