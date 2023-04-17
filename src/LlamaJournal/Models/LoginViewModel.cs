using System.ComponentModel.DataAnnotations;


namespace llama_journal.Models;

public class LoginViewModel
{
	[Required]
	[EmailAddress]
	public string Email { get; set; }

	[Required]
	[DataType(DataType.Password)]
	public string Password { get; set; }

	public bool RememberMe { get; set; }
}

public class SignupViewModel
{
	[Required]
	[EmailAddress]
	public string Email { get; set; }

	[Required]
	[Display(Name = "Full Name")]
	public string FullName { get; set; }

	[Required]
	[DataType(DataType.Password)]
	[Display(Name = "Password")]
	public string Password { get; set; }

	[DataType(DataType.Password)]
	[Display(Name = "Confirm password")]
	[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
	public string ConfirmPassword { get; set; }
}
