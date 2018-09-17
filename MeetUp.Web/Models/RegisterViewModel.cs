namespace MeetUp.Web.Models
{
    using MeetUp.Data.Models;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        //[Display(Name = "Emailll")]
        public string Email { get; set; }

        [Required]
        //[Display(Name = "Full name")]
        public string FullName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        //[Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        //[Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Range(1, 3, ErrorMessage = "Select a correct sex")]
        public UserSex Sex { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Location minimum length is 3")]
        public string Location { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

    }
}