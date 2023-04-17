using BlogApi.Dtos;

namespace Services.Interface
{
    public interface ICategoryService
    {
        Task CreateAsync(CategoryDto categoryDto);
        Task UpdateAsync(CategoryDto categoryDto);
        Task DeleteAsync(CategoryDto categoryDto);
    }
}