using BlogApi.Models;

namespace Repository.Interface
{
    public interface IPostRepository : IRepository<Post>
    {
        bool SlugExists(string slug);
        Task<List<Post>> GetPostsWithApplicationUserAndCategory();
        Task<Post> GetPostWithApplicationUserAndCategory(int id);
    }
}