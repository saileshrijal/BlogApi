using BlogApi.Dtos;

namespace BlogApi.Services.Interface
{
    public interface IPostService
    {
        Task CreateAsync(PostDto postDto, List<int> categoryIds);
        Task UpdateAsync(int id, PostDto postDto, List<int> categoryIds);
        Task DeleteAsync(int id);
    }
}