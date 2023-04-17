using BlogApi.Dtos;
using BlogApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Repository.Interface;
using Services.Interface;

namespace BlogApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly ICategoryRepository _categoryRepository;
    public CategoryController(ICategoryService categoryService, ICategoryRepository categoryRepository)
    {
        _categoryService = categoryService;
        _categoryRepository = categoryRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var categories = await _categoryRepository.GetAll();
            return Ok(categories);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var category = await _categoryRepository.GetById(id);
            return Ok(category);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CategoryVM vm)
    {
        try
        {
            var slug = vm.Name!.Trim().Replace(" ", "-").ToLower();
            if (_categoryRepository.SlugExists(slug))
            {
                ModelState.AddModelError("Slug", "The slug must be unique");
                return BadRequest(ModelState);
            }
            var categoryDto = new CategoryDto
            {
                Name = vm.Name,
                Description = vm.Description,
                Slug = vm.Name!.Trim().Replace(" ", "-").ToLower()
            };
            await _categoryService.CreateAsync(categoryDto);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CategoryVM vm)
    {
        try
        {
            var category = await _categoryRepository.Get(x => x.Id == id);
            var slug = vm.Name!.Trim().Replace(" ", "-").ToLower();
            if (_categoryRepository.SlugExists(slug) && category.Slug != slug)
            {
                ModelState.AddModelError("Slug", "The slug must be unique");
                return BadRequest(ModelState);
            }
            var categoryDto = new CategoryDto
            {
                Id = category.Id,
                Name = vm.Name,
                Description = vm.Description,
                Slug = vm.Name!.Trim().Replace(" ", "-").ToLower()
            };
            await _categoryService.UpdateAsync(categoryDto);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var category = await _categoryRepository.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            var categoryDto = new CategoryDto
            {
                Id = category.Id,
                Name = category.Title,
                Description = category.Description,
                Slug = category.Slug
            };
            await _categoryService.DeleteAsync(categoryDto);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}