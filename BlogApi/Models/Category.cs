namespace BlogApi.Models;

public class Category
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedDate { get; set; }

    //navigation property
    public List<PostCategory>? PostCategories { get; set; }
}