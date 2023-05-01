namespace LlamaJournal.Models
{
    using System.ComponentModel.DataAnnotations;

    public class AdminCompleteRegistrationModel
    {
        public AdminCompleteRegistrationModel(string fullName, string organizationName)
        {
            this.FullName = fullName;
            this.OrganizationName = organizationName;
        }

        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "Organization Name")]
        public string OrganizationName { get; set; }
    }
}