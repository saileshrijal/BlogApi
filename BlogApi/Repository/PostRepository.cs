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
        public bool SlugExists(string slug)
        {
            return _context.Posts!.Any(c => c.Slug == slug);
        }

        public async Task<List<Post>> GetPostsWithApplicationUserAndCategory()
        {
            return await _context.Posts!.Include(c => c.ApplicationUser).Include(c => c.PostCategories)!.ThenInclude(c => c.Category).ToListAsync() ?? new List<Post>();
        }

        public async Task<Post> GetPostWithApplicationUserAndCategory(int id)
        {
            return await _context.Posts!.Include(c => c.ApplicationUser).Include(c => c.PostCategories)!.ThenInclude(c => c.Category).FirstOrDefaultAsync(x => x.Id == id) ?? new Post();
        }
    }
}