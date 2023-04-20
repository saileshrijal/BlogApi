using System.ComponentModel.DataAnnotations;

namespace BlogApi.ViewModels
{
    public class UserVM
    {
        [Required(ErrorMessage = "Username is required")]
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string? UserName { get; set; }
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", ErrorMessage = "Password must be between 8 and 15 characters, contain at least one uppercase letter, one lowercase letter, one number and one special character")]
        public string? Password { get; set; }
    }
}
