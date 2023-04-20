using BlogApi.Data;
using BlogApi.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;

namespace Repository
{
    public class PostCategoryRepository : Repository<PostCategory>, IPostCategoryRepository
    {
        public PostCategoryRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<PostCategory>> GetPostCategoriesByPostId(int postId)
        {
            return await _context.PostCategories!.AsNoTracking().Where(x => x.PostId == postId).ToListAsync();
        }
    }
}