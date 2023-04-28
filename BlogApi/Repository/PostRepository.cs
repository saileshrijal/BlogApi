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
            return await _context.Posts!.Include(c => c.ApplicationUser).Include(c => c.PostCategories)!.ThenInclude(c => c.Category).OrderByDescending(x => x.CreatedDate).ToListAsync() ?? new List<Post>();
        }

        public async Task<List<Post>> GetPublishedPostsByCategoryId(int categoryId)
        {
            return await _context.Posts!.Include(c => c.ApplicationUser).Include(c => c.PostCategories)!.ThenInclude(c => c.Category).Where(x => x.PostCategories!.Any(c => c.CategoryId == categoryId) && x.IsPublished).OrderByDescending(x => x.CreatedDate).ToListAsync() ?? new List<Post>();
        }

        public async Task<Post> GetPostWithApplicationUserAndCategory(int id)
        {
            return await _context.Posts!.Include(c => c.ApplicationUser).Include(c => c.PostCategories)!.ThenInclude(c => c.Category).FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception("Post not found");
        }

        public async Task<List<Post>> GetPublishedPosts()
        {
            return await _context.Posts!.Include(c => c.ApplicationUser).Include(c => c.PostCategories)!.ThenInclude(c => c.Category).Where(x=>x.IsPublished).OrderByDescending(x => x.CreatedDate).ToListAsync() ?? new List<Post>();
        }

        public async Task<Post> GetPublishedPost(int id)
        {
            return await _context.Posts!.Include(c => c.ApplicationUser).Include(c => c.PostCategories)!.ThenInclude(c => c.Category).FirstOrDefaultAsync(x => x.Id == id && x.IsPublished) ?? throw new Exception("Post not found");
        }

        public async Task<Post> GetPublishedPostBySlug(string slug)
        {
            return await _context.Posts!.Include(c => c.ApplicationUser).Include(c => c.PostCategories)!.ThenInclude(c => c.Category).FirstOrDefaultAsync(x => x.Slug == slug && x.IsPublished) ?? throw new Exception("Post not found");
        }

        public async Task<List<Post>> GetRecentPublishedPosts()
        {
            return await _context.Posts!.Include(c => c.ApplicationUser).Include(c => c.PostCategories)!.ThenInclude(c => c.Category).Where(x => x.IsPublished).OrderByDescending(x=>x.CreatedDate).Take(3).ToListAsync() ?? new List<Post>();
        }
    }
}