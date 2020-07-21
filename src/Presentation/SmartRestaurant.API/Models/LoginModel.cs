﻿using System.ComponentModel.DataAnnotations;

namespace SmartRestaurant.API.Models
{
    public class LoginModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}