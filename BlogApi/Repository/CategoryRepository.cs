using BlogApi.Data;
using BlogApi.Models;
using Repository.Interface;

namespace Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }
        public bool SlugExists(string slug)
        {
            return _context.Categories!.Any(c => c.Slug == slug);
        }
    }
}