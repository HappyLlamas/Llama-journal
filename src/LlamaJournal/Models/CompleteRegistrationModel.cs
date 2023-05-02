namespace LlamaJournal.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CompleteRegistrationModel
    {
        public CompleteRegistrationModel(string password, string confirmPassword)
        {
            this.Password = password;
            this.ConfirmPassword = confirmPassword;
        }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}