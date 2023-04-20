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
    }
}