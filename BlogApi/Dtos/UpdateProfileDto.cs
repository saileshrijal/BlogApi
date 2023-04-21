using System.ComponentModel.DataAnnotations;

namespace BlogApi.Dtos
{
    public class UpdateProfileDto
    {
        public string? UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
