namespace BlogApi.ViewModels
{
    public class CategoryVM
    {
        public int Id { get; set; }
        public required string? Name { get; set; }
        public string? Description { get; set; }
        public string? Slug { get; set; }
    }
}