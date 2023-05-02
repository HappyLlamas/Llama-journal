using DataLayer.Repositories;
using DataLayer.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace BusinnesLayer.Services;

public class LoginService: ILoginService
{
    private readonly IUserRepository _userRepository;
    private readonly IOrganizationRepository _organizationRepository;

    public LoginService(IUserRepository userRepository, IOrganizationRepository organizationRepository)
    {
        _userRepository = userRepository;
		_organizationRepository = organizationRepository;
    }

    public async Task CompleteRegistration(string userId, string password, string confirmPassword)
    {
        var user = await _userRepository.GetById(userId);

		if(user == null)
			throw new InvalidDataException($"User with id {userId} not found");

		if (password != confirmPassword)
        	throw new InvalidDataException("Паролі не співпадають");

		// TODO: hash password
		user.Password = password;
		user.CompleteRegistration = true;
        await _userRepository.Update(user);
    }

    public async Task AdminCompleteRegistration(string userId, string fullName, string organizationName)
    {
        var user = await _userRepository.GetById(userId);

		if(user == null)
			throw new InvalidDataException($"User with id {userId} not found");

		await _organizationRepository.CreateOrganization(organizationName, user);
		user.FullName = fullName;
		user.CompleteRegistration = true;

		await _userRepository.Update(user);
    }

    public async Task SignUp(string email, string password, string confirmPassword)
    {
		if (password != confirmPassword)
        	throw new InvalidDataException("Паролі не співпадають");

		if (_userRepository.FindByEmail(email) != null)
			throw new InvalidDataException("Юзер з таким email вже існує");

	    await _userRepository.CreateUser( new User{Id = new Guid().ToString(),  Email = email, 
		    FullName= "Admin",  Role = RoleEnum.Admin,  Password = password});
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
