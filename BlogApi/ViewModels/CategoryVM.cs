using System.ComponentModel.DataAnnotations;

namespace BlogApi.ViewModels
{
    public class CategoryVM
    {
        [Required(ErrorMessage = "Title is required")]
        public required string? Title { get; set; }
        public string? Description { get; set; }
    }
}
