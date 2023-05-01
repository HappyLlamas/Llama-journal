// <copyright file="LoginViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LlamaJournal.Models
{
    using System.ComponentModel.DataAnnotations;

    public class LoginViewModel
    {
        public LoginViewModel(string email, string password, bool rememberMe)
        {
            this.Email = email;
            this.Password = password;
            this.RememberMe = rememberMe;
        }

        [EmailAddress]
        [Required(ErrorMessage = "incorrect email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "incorrect password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}