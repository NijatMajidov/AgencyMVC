﻿using System.ComponentModel.DataAnnotations;

namespace AgencyMVC.DTOs
{
    public class RegisterDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Surname { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string UserName { get; set; }
        [Required]
        [MinLength(6)]
        [MaxLength(100)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [MaxLength(100)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password),Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
