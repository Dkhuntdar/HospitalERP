using System.ComponentModel.DataAnnotations;

namespace HospitalERP.Entities;

public class Doctor
{
    [Key]
    public int DoctorId { get; set; }

    // 👇 Foreign Key to User
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public string DoctorName { get; set; } = null!;
    public string Specialization { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public decimal ConsultationFee { get; set; }
    public string? RoomNumber { get; set; }
}