using System.ComponentModel.DataAnnotations;

namespace HospitalERP.DTOs;

public class CreateDoctorDto
{
    // User Account Credentials
    [Required, EmailAddress]
    public string Email { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;

    // Doctor Professional Details
    [Required]
    public string DoctorName { get; set; } = null!;
    [Required]
    public string Specialization { get; set; } = null!;
    [Required, Phone]
    public string Phone { get; set; } = null!;
    [Required]
    public decimal ConsultationFee { get; set; }
    public string? RoomNumber { get; set; }
}

public class DoctorResponseDto
{
    public int DoctorId { get; set; }
    public string DoctorName { get; set; } = null!;
    public string Specialization { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public decimal ConsultationFee { get; set; }
    public string? RoomNumber { get; set; }
    public string Email { get; set; } = null!; // User table se aayega
}