using System;
using System.ComponentModel.DataAnnotations;

namespace Capsell.Models.Authenticate
{
	public class RegisterModel
	{
        [Required(ErrorMessage = "Username is required")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Mobile is required")]
        public string? Mobile { get; set; }

        public string? Address { get; set; }
    }
}

