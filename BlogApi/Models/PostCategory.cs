namespace BlogApi.Models
{
    public class PostCategory
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; } = new Post();
        public int CategoryId { get; set; }
        public Category Category { get; set; } = new Category();
    }
}