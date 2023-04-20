using BlogApi.Dtos;
using BlogApi.Models;
using Repository.Interface;
using Services.Interface;

namespace Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(IUnitOfWork unitOfWork, ICategoryRepository categoryRepository)
        {
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
        }

        public async Task CreateAsync(CategoryDto categoryDto)
        {
            var category = new Category
            {
                Title = categoryDto.Title,
                Description = categoryDto.Description,
                CreatedDate = DateTime.Now,
            };
            await _unitOfWork.CreateAsync(category);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _categoryRepository.GetById(id);
            await _unitOfWork.DeleteAsync(category);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(int id, CategoryDto categoryDto)
        {
            var category = await _categoryRepository.GetById(id);
            category.Title = categoryDto.Title;
            category.Description = categoryDto.Description;
            await _unitOfWork.SaveAsync();
        }
    }
}