using System.ComponentModel.DataAnnotations;

namespace BlogApi.ViewModels
{
    public class PostVM
    {
        [Required(ErrorMessage = "Title is required")]
        public string? Title { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string? ShortDescription { get; set; }

        public bool IsPublished { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public List<int>? CategoryIds { get; set; }

        public IFormFile? Thumbnail { get; set; }
    }
}
