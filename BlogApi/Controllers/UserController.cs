using BlogApi.Constants;
using BlogApi.Dtos;
using BlogApi.Models;
using BlogApi.Services.Interface;
using BlogApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Authorize(Roles = UserRole.Admin)]
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
    public async Task<IActionResult> Get()
    {
        var users = await _userManager.GetUsersInRoleAsync(UserRole.User);
        var result = users.Select(x => new
        {
            x.Id,
            x.FirstName,
            x.LastName,
            x.UserName,
            x.Email,
            x.Status,
            role = _userManager.GetRolesAsync(x).Result.FirstOrDefault()
        });
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var user = await _userManager.FindByNameAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        var result = new
        {
            user!.Id,
            user.FirstName,
            user.LastName,
            user.UserName,
            user.Email,
            user.Status,
            role = _userManager.GetRolesAsync(user).Result.FirstOrDefault()
        };
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
                UserRole = UserRole.User,
                Status = true
            };
            await _userService.Create(userDto);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Edit(string id, UserVM vm)
    {
        try
        {
            var userDto = new UserDto
            {
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                UserName = vm.UserName,
                Email = vm.Email,
            };
            await _userService.Update(id, userDto);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{id}")]
    public async Task<IActionResult> ToggleUserStatus(string id)
    {
        try
        {
            await _userService.ToggleUserStatus(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}