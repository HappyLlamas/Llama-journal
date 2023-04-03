using DataLayer.Repositories;
using DataLayer.Models;

namespace BusinnesLayer.Services;

public class LoginService
{
    private readonly IUserRepository _userRepository;

    public LoginService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public User SignUp(String email, String password)
    {
        var user = _userRepository.FindByEmail(email);
        _userRepository.SetUserPassword(user, password);
        return user;
    }
    public User Login(String email, String password)
    {
        var user = _userRepository.FindByEmail(email);
        var result = _userRepository.CheckPassword(user, password);

        return user;
    }
    public void SendResetPasswodEmail(String email)
    {
        var user = _userRepository.FindByEmail(email);
        // TODO: generate and send token
    }
    public void ResetPassword(String email, String password, String restore_token)
    {
        var user = _userRepository.FindByEmail(email);
        // TODO: check restore_token
        bool restore_token_is_valid = true;
        if (!restore_token_is_valid)
            throw new ArgumentException("Restore token is incorrect");

        _userRepository.SetUserPassword(user, password);
    }
}
