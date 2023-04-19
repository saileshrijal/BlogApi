using Manager.Interface;
using Microsoft.AspNetCore.Mvc;
using ViewModels;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthManager _authManager;

        public AuthController(IAuthManager authManager)
        {
            _authManager = authManager;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginVM vm)
        {
            var result = await _authManager.Login(vm.Username!, vm.Password!);
            return Ok(result);
        }
    }
}