using BlogApi.Seeder.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SeedController : ControllerBase
{
    private readonly IUserSeeder _userSeeder;
    public SeedController(IUserSeeder userSeeder)
    {
        _userSeeder = userSeeder;
    }

    [HttpPost]
    public async Task<IActionResult> SeedAdminUser()
    {
        try
        {
            await _userSeeder.SeedAdminUser();
            return Ok("Admin user seeded successfully");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}