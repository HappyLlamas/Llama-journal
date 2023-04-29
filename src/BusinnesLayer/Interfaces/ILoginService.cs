using DataLayer.Models;
using System.Security.Claims;

namespace BusinnesLayer.Services;

public interface ILoginService
{
    Task SignUp(string fullName, string email);
    Task<ClaimsIdentity> Login(String email, String password);
    Task SendResetPasswodEmail(String email);
    Task ResetPassword(String email, String password, String restore_token);
}
