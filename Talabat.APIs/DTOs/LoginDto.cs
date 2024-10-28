using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.DTOs;

public class LoginDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    public string Password { get; set; }
}