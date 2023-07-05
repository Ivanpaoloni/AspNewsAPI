using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AspNewsAPI.DTOs
{
    public class UserCredentials
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
