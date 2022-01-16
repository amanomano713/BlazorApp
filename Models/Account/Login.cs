using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models.Account
{
    public class Login
    {
        [Required]
        public string Email { get; set; }

    }
}