using BlogApi.Dtos;
using BlogApi.Models;
using BlogApi.Services.Interface;
using BlogApi.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly UserManager<ApplicationUser> _userManager;

    public UserController(IUserService userService, UserManager<ApplicationUser> userManager)
    {
        _userService = userService;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var users = await _userManager.Users.ToListAsync();
        var result = users.Select(x => new
        {
            x.Id,
            x.FirstName,
            x.LastName,
            x.UserName,
            x.Email,
            x.PhoneNumber,
            role = _userManager.GetRolesAsync(x).Result.FirstOrDefault()
        });
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(UserVM vm)
    {
        try
        {
            var userDto = new UserDto
            {
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                UserName = vm.UserName,
                Email = vm.Email,
                Password = vm.Password,
                UserRole = vm.UserRole
            };
            await _userService.Create(userDto);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}