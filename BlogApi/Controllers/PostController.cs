using System.Security.Claims;
using BlogApi.Dtos;
using BlogApi.Helper.Interface;
using BlogApi.Models;
using BlogApi.Services.Interface;
using BlogApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repository.Interface;

namespace BlogApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Authorize]
public class PostController : ControllerBase
{

    private readonly IPostService _postService;
    private readonly IPostRepository _postRepository;
    private readonly IFileHelper _fileHelper;
    private readonly UserManager<ApplicationUser> _userManager;

    public PostController(IPostService postService, IPostRepository postRepository, IFileHelper fileHelper, UserManager<ApplicationUser> userManager)
    {
        _postService = postService;
        _postRepository = postRepository;
        _fileHelper = fileHelper;
        _userManager = userManager;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] PostVM vm)
    {
        try
        {
            var currentUser = await CurrentUser();
            var postDto = new PostDto
            {
                Title = vm.Title,
                Description = vm.Description,
                ShortDescription = vm.ShortDescription,
                ApplicationUserId = currentUser.Id,
                IsPublished = vm.IsPublished,
            };
            if (vm.Thumbnail != null)
            {
                var thumbnailUrl = await _fileHelper.Save(vm.Thumbnail, "post");
                postDto.ThumbnailUrl = thumbnailUrl;
            }
            await _postService.CreateAsync(postDto, vm.CategoryIds!);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Edit(int id, [FromForm] PostVM vm)
    {
        try
        {
            var post = await _postRepository.Get(x => x.Id == id);
            var currentUser = await CurrentUser();
            var userRole = await CurrentUserRole();
            if (userRole != "Admin" && post!.ApplicationUserId != currentUser.Id)
            {
                return Unauthorized();
            }
            var postDto = new PostDto
            {
                Title = vm.Title,
                Description = vm.Description,
                ShortDescription = vm.ShortDescription,
                IsPublished = vm.IsPublished,
            };
            if (vm.Thumbnail != null)
            {
                var thumbnailUrl = await _fileHelper.Save(vm.Thumbnail, "post");
                postDto.ThumbnailUrl = thumbnailUrl;
            }
            await _postService.UpdateAsync(id, postDto, vm.CategoryIds!);
            return Ok();
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
            var post = await _postRepository.GetPostWithApplicationUserAndCategory(id);
            var result = new
            {
                post.Id,
                post.Title,
                post.ShortDescription,
                post.Description,
                post.ThumbnailUrl,
                Author = post.ApplicationUser!.FirstName + post.ApplicationUser!.LastName,
                post.Slug,
                post.CreatedDate,
                post.IsPublished,
                Categories = post.PostCategories!.Select(c => c.Category!.Title)
            };
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var posts = await _postRepository.GetPostsWithApplicationUserAndCategory();
            var result = posts.Select(x => new
            {
                x.Id,
                x.Title,
                x.ShortDescription,
                x.Description,
                x.ThumbnailUrl,
                Author = x.ApplicationUser!.FirstName + x.ApplicationUser!.LastName,
                x.Slug,
                x.CreatedDate,
                x.IsPublished,
                Categories = x.PostCategories!.Select(c => c.Category!.Title)
            });
            return Ok(result);
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
            await _postService.DeleteAsync(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    private async Task<ApplicationUser> CurrentUser()
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity!;
        var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
        var username = claims!.Value;
        return await _userManager.FindByNameAsync(username) ?? new ApplicationUser();
    }

    private async Task<string> CurrentUserRole()
    {
        var currentUser = await CurrentUser();
        var roles = await _userManager.GetRolesAsync(currentUser);
        return roles.FirstOrDefault() ?? "";
    }
}