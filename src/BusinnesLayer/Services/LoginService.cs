using DataLayer.Repositories;
using DataLayer.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace BusinnesLayer.Services;

public class LoginService: ILoginService
{
    private readonly IUserRepository _userRepository;

    public LoginService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> SetPassword(string email, string password)
    {
        var user = await _userRepository.FindByEmail(email);

		if(user == null)
			throw new InvalidDataException($"User with email {email} not found");

		// TODO: hash password
		user.Password = password;
        await _userRepository.Update(user);

        return user;
    }

    public async Task<User> SignUp(string email, string fullName)
    {
        var user = await _userRepository.FindByEmail(email);

		if(user == null)
			throw new InvalidDataException($"User with email {email} not found");

		user.FullName = fullName;
        await _userRepository.Update(user);

        return user;
    }
    public async Task<ClaimsIdentity> Login(String email, String password)
	{
        var user = await _userRepository.FindByEmail(email);

		// TODO: unhash password
		if(user == null || user.Password != password)
			throw new InvalidDataException($"Incorrect email or password");

		var claims = new List<Claim>
		{
			new Claim(ClaimTypes.Name, user.Id),
			new Claim(ClaimTypes.Email, user.Email),
			new Claim(ClaimTypes.Role, user.Role.ToString())
		};

		var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        return claimsIdentity;
    }
    public async Task SendResetPasswodEmail(String email)
    {
        var user = await _userRepository.FindByEmail(email);
        // TODO: generate and send token
    }
    public async Task ResetPassword(String email, String password, String restore_token)
    {
        var user = await _userRepository.FindByEmail(email);

		if(user == null)
			throw new Exception($"User with email {email} not found");

        // TODO: check restore_token
        bool restore_token_is_valid = true;
        if (!restore_token_is_valid)
            throw new ArgumentException("Restore token is incorrect");

		user.Password = password;
        await _userRepository.Update(user);
    }
}
