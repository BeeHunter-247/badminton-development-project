﻿using Badminton.Web.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Badminton.Web.DTO.OTP
{
    public class SendOtpModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required]
        [PhoneNumber]
        public string Phone { get; set; }
        public string Otp { get; set; }
        public DateTime OtpExpiration { get; set; }
    }
}
