namespace BlogApi.Models;

public class Post
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Slug { get; set; }
    public string? Description { get; set; }
    public string? ShortDescription { get; set; }
    public string? ThumbnailUrl { get; set; }
    public DateTime? CreatedDate { get; set; }
    public bool IsPublished { get; set; }

    public string? ApplicationUserId { get; set; }
    public ApplicationUser? ApplicationUser { get; set; }

    //Navigation Property
    public List<PostCategory>? PostCategories { get; set; }
}