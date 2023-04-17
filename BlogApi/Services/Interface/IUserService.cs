using BlogApi.Dtos;
using BlogApi.Models;

namespace BlogApi.Services.Interface;

public interface IUserService
{
    Task<ApplicationUser> Create(UserDto userDto);
}