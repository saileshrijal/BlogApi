using BlogApi.Models;

namespace Repository.Interface
{
    public interface ICategoryRepository : IRepository<Category>
    {
        bool SlugExists(string slug);
    }
}