﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutoPark.Api.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
         
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
         
        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }
         
        public string ReturnUrl { get; set; }
        
        public string UserName { get; set; }
        

        public List<string> Roles { get; set; } = new();
    }
}