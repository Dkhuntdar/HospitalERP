using System.ComponentModel.DataAnnotations;

namespace HospitalERP.DTOs;

public class LoginRequestDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}

public class RegisterRequestDto : LoginRequestDto
{
    [Required]
    public string Role { get; set; } = "Receptionist"; // Admin, Doctor, Receptionist
}

public class AuthResponseDto
{
    public string Token { get; set; } = null!;
    public string Role { get; set; } = null!;
    public string Email { get; set; } = null!;
}