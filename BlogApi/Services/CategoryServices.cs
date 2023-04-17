using BlogApi.Dtos;
using BlogApi.Models;
using Repository.Interface;
using Services.Interface;

namespace Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateAsync(CategoryDto categoryDto)
        {
            var category = new Category
            {
                Title = categoryDto.Name,
                Description = categoryDto.Description,
                CreatedDate = DateTime.Now,
                Slug = categoryDto.Name!.Trim().Replace(" ", "-").ToLower()
            };
            await _unitOfWork.CreateAsync(category);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(CategoryDto categoryDto)
        {
            var category = new Category
            {
                Id = categoryDto.Id,
                Title = categoryDto.Name,
                Description = categoryDto.Description,
                CreatedDate = DateTime.Now,
                Slug = categoryDto.Slug
            };

            await _unitOfWork.DeleteAsync(category);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(CategoryDto categoryDto)
        {
            var category = new Category
            {
                Id = categoryDto.Id,
                Title = categoryDto.Name,
                Description = categoryDto.Description,
                CreatedDate = DateTime.Now,
                Slug = categoryDto.Slug
            };
            await _unitOfWork.UpdateAsync(category);
            await _unitOfWork.SaveAsync();
        }
    }
}