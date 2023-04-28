using BlogApi.Constants;
using BlogApi.Dtos;
using BlogApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Interface;
using Services.Interface;

namespace BlogApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Authorize(Roles = UserRole.Admin)]
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
    [AllowAnonymous]
    public async Task<IActionResult> Get()
    {
        try
        {
            var categories = await _categoryRepository.GetAll();
            var result = categories.Select(x => new
            {
                x.Id,
                x.Title,
                x.Description,
                x.CreatedDate
            });
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var category = await _categoryRepository.GetById(id);
            var result = new
            {
                category.Id,
                category.Title,
                category.Description,
                category.CreatedDate
            };
            return Ok(result);
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
            var categoryDto = new CategoryDto
            {
                Title = vm.Title,
                Description = vm.Description,
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
    public async Task<IActionResult> Edit(int id, [FromBody] CategoryVM vm)
    {
        try
        {
            var categoryDto = new CategoryDto
            {
                Title = vm.Title,
                Description = vm.Description,
            };
            await _categoryService.UpdateAsync(id, categoryDto);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _categoryService.DeleteAsync(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}