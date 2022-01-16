using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models
{
    public class SignInModel
    {
        [Required(ErrorMessage = "Email es Obligatorio")]
        [StringLength(30, MinimumLength = 8,
                  ErrorMessage = "El Email debe tener entre 8 y 30 caracteres")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password es Obligatorio")]
        [StringLength(10, MinimumLength = 6,
          ErrorMessage = "El Password debe tener entre 6 y 10 caracteres")]
        public string? Password { get; set; }
    }
}
