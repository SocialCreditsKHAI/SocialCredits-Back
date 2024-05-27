using SocialCredits.Domain.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace SocialCredits.Domain.ViewModels
{
    public class UserRegistrationViewModel
    {
        [Required(ErrorMessage = "Login is required")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "The login must contain from 4 to 50 characters")]
        public string Login { get; set; }

        [StringLength(50, MinimumLength = 4, ErrorMessage = "The name must contain from 4 to 50 characters")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Password is a required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "The password must contain from 6 to 100 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password confirmation is a mandatory field")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        public List<Socials> Socials { get; set; }
        
    }

    public class UserRegistrationWithImageViewModel : UserRegistrationViewModel
    {
        [Required(ErrorMessage = "Image is a required field")]
        public IFormFile Image { get; set; }
    }
    public class UserRegistrationWithImageNameViewModel : UserRegistrationViewModel
    {
        
        public string imageName { get; set; }
    }
}
