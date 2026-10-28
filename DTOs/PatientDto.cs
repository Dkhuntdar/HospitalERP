using System.ComponentModel.DataAnnotations;

namespace HospitalERP.DTOs;

public class CreatePatientDto
{
    [Required(ErrorMessage = "Patient Name is required")]
    public string PatientName { get; set; } = null!;

    [Required]
    [Phone]
    public string Phone { get; set; } = null!;

    [Required]
    public string Gender { get; set; } = null!; // Male, Female, Other

    [Range(0, 150)]
    public int Age { get; set; }

    public string? BloodGroup { get; set; }
    public string? Address { get; set; }
}

public class PatientResponseDto
{
    public int PatientId { get; set; }
    public string UHID { get; set; } = null!;
    public string PatientName { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Gender { get; set; } = null!;
    public int Age { get; set; }
    public string? BloodGroup { get; set; }
    public string? Address { get; set; }
    public DateTime RegisteredAt { get; set; }
}