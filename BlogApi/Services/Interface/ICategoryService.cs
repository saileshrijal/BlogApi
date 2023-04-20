using BlogApi.Dtos;

namespace Services.Interface
{
    public interface ICategoryService
    {
        Task CreateAsync(CategoryDto categoryDto);
        Task UpdateAsync(int id, CategoryDto categoryDto);
        Task DeleteAsync(int id);
    }
}