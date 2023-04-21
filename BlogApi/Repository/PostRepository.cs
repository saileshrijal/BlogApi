using BlogApi.Data;
using BlogApi.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;

namespace Repository
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<Post>> GetPostsWithApplicationUserAndCategory()
        {
            var results = await _context.Posts!.Include(c => c.ApplicationUser).Include(c => c.PostCategories)!.ThenInclude(c => c.Category).ToListAsync() ?? new List<Post>();
            return results;
        }

        public async Task<Post> GetPostWithApplicationUserAndCategory(int id)
        {
            return await _context.Posts!.Include(c => c.ApplicationUser).Include(c => c.PostCategories)!.ThenInclude(c => c.Category).FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception("Post not found");
        }
    }
}