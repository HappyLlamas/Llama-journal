using System.Security.Claims;

namespace BusinnesLayer.Services;

public interface ILoginService
{
    Task<ClaimsIdentity> Login(String email, String password);
    Task SendResetPasswodEmail(String email);
    Task ResetPassword(String email, String password, String restore_token);
    Task SignUp(string email, string password, string confirmPassword);
    Task CompleteRegistration(string userId, string password, string confirmPassword);
    Task AdminCompleteRegistration(string userId, string fullName, string organizationName);
}
