using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class RegisterRequest
{
    [Required]
    public string DisplayName { get; set; } = string.Empty;
    [Required]
    [EmailAddress] //Validacion de formato de email
    public string Email { get; set; } = string.Empty;
    [Required]
    [MinLength(6)] //Validacion de longitud minima
    public string Password { get; set; } = string.Empty;
}
