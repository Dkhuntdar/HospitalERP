using System.ComponentModel.DataAnnotations;

namespace HospitalERP.DTOs;

public class CreateAppointmentDto
{
    [Required]
    public int PatientId { get; set; }

    [Required]
    public int DoctorId { get; set; }

    [Required]
    public DateTime AppointmentDate { get; set; }

    public string? Notes { get; set; }
}

public class AppointmentResponseDto
{
    public int AppointmentId { get; set; }
    public string PatientName { get; set; } = null!;
    public string DoctorName { get; set; } = null!;
    public DateTime AppointmentDate { get; set; }
    public string Status { get; set; } = null!;
    public string? Notes { get; set; }
}