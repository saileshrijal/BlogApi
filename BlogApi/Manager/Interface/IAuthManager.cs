using BlogApi.Result;

namespace Manager.Interface;

public interface IAuthManager
{
    Task<AuthResult> Login(string username, string password);
}