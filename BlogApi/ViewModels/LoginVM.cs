using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Username is required")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}