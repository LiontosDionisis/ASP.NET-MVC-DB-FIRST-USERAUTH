﻿using System.ComponentModel.DataAnnotations;

namespace TeachersMVC.DTO
{
    public class UserPatchDTO
    {
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Username should be between 2-50 characters.")]
        public string? Username{ get; set; }

        [StringLength(100, ErrorMessage = "Email should not exceed 100 characters")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? Email{ get; set; }


        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\W).{8,}$",
            ErrorMessage = "Password must contain at least one uppercase letter, " + "one lowercase letter, one digit and one special character")]
        public string? Password { get; set; }

        [StringLength(15, ErrorMessage = "Phone number should not exceed 15 characters")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string? PhoneNumber { get; set; }
    }
}
