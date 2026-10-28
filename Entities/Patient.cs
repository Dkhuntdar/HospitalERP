using System.ComponentModel.DataAnnotations;

namespace HospitalERP.Entities;

public class Patient
{
    [Key]
    public int PatientId { get; set; }

    // UHID = Unique Hospital ID (jaise "UHID-2026-001")
    public string UHID { get; set; } = null!;

    public string PatientName { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Gender { get; set; } = null!;
    public int Age { get; set; }
    public string? BloodGroup { get; set; }
    public string? Address { get; set; }

    public DateTime RegisteredAt { get; set; } = DateTime.Now;
}