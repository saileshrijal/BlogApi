using BlogApi.Models;

namespace Repository.Interface
{
    public interface IPostCategoryRepository : IRepository<PostCategory>
    {
        Task<List<PostCategory>> GetPostCategoriesByPostId(int postId);
    }
}