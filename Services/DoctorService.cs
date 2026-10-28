using HospitalERP.Data;
using HospitalERP.DTOs;
using HospitalERP.Entities;
using Microsoft.EntityFrameworkCore;

namespace HospitalERP.Services;

public class DoctorService : IDoctorService
{
    private readonly AppDbContext _db;

    public DoctorService(AppDbContext db) => _db = db;

    public async Task<DoctorResponseDto> CreateDoctorAsync(CreateDoctorDto dto)
    {
        // 1. Check if email is already taken
        if (await _db.Users.AnyAsync(u => u.Email == dto.Email))
            throw new InvalidOperationException("A user with this email already exists.");

        // 2. Create the User Account (Role = Doctor)
        var user = new User
        {
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Role = "Doctor"
        };
        _db.Users.Add(user);
        await _db.SaveChangesAsync(); // Save here so UserId is generated

        // 3. Create the Doctor Profile
        var doctor = new Doctor
        {
            UserId = user.UserId,
            DoctorName = dto.DoctorName,
            Specialization = dto.Specialization,
            Phone = dto.Phone,
            ConsultationFee = dto.ConsultationFee,
            RoomNumber = dto.RoomNumber
        };
        _db.Doctors.Add(doctor);
        await _db.SaveChangesAsync();

        return new DoctorResponseDto
        {
            DoctorId = doctor.DoctorId,
            DoctorName = doctor.DoctorName,
            Specialization = doctor.Specialization,
            Phone = doctor.Phone,
            ConsultationFee = doctor.ConsultationFee,
            RoomNumber = doctor.RoomNumber,
            Email = user.Email
        };
    }

    public async Task<IEnumerable<DoctorResponseDto>> GetAllDoctorsAsync()
    {
        return await _db.Doctors
            .Include(d => d.User)
            .Select(d => new DoctorResponseDto
            {
                DoctorId = d.DoctorId,
                DoctorName = d.DoctorName,
                Specialization = d.Specialization,
                Phone = d.Phone,
                ConsultationFee = d.ConsultationFee,
                RoomNumber = d.RoomNumber,
                Email = d.User.Email
            })
            .ToListAsync();
    }
}