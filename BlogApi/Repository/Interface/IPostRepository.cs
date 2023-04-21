using BlogApi.Models;

namespace Repository.Interface
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<List<Post>> GetPostsWithApplicationUserAndCategory();
        Task<Post> GetPostWithApplicationUserAndCategory(int id);
    }
}