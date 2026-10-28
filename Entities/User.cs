using System.ComponentModel.DataAnnotations;

namespace HospitalERP.Entities;

public class User
{
    [Key]
    public int UserId { get; set; }

    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;

    // Roles: "Admin", "Doctor", "Receptionist"
    public string Role { get; set; } = "Receptionist";

    public DateTime CreatedAt { get; set; } = DateTime.Now;
}