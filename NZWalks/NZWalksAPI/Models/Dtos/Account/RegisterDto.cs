﻿using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.Models.Dtos.Account
{
    public class RegisterDto
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; }= string.Empty;

        [Required]
        public string Password { get; set; }

        public string[] Roles { get; set; }
    }
}
