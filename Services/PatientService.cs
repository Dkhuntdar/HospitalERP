using HospitalERP.Data;
using HospitalERP.DTOs;
using HospitalERP.Entities;
using Microsoft.EntityFrameworkCore;

namespace HospitalERP.Services;

public class PatientService : IPatientService
{
    private readonly AppDbContext _db;

    public PatientService(AppDbContext db) => _db = db;

    public async Task<PatientResponseDto> RegisterPatientAsync(CreatePatientDto dto)
    {
        // 🛑 Business Rule: Phone number pehle se exist karta hai kya? (Optional)
        if (await _db.Patients.AnyAsync(p => p.Phone == dto.Phone))
            throw new InvalidOperationException("A patient with this phone number already exists.");

        // 🛑 UHID Auto-Generate karo (Format: UHID-2026-0001)
        var currentYear = DateTime.UtcNow.Year;
        var countThisYear = await _db.Patients.CountAsync(p => p.RegisteredAt.Year == currentYear);
        var uhid = $"UHID-{currentYear}-{(countThisYear + 1):D4}";

        var patient = new Patient
        {
            UHID = uhid,
            PatientName = dto.PatientName,
            Phone = dto.Phone,
            Gender = dto.Gender,
            Age = dto.Age,
            BloodGroup = dto.BloodGroup,
            Address = dto.Address,
            RegisteredAt = DateTime.UtcNow
        };

        _db.Patients.Add(patient);
        await _db.SaveChangesAsync();

        return MapToDto(patient);
    }

    public async Task<IEnumerable<PatientResponseDto>> GetAllPatientsAsync()
    {
        var patients = await _db.Patients
            .OrderByDescending(p => p.RegisteredAt)
            .ToListAsync();

        return patients.Select(MapToDto);
    }

    public async Task<PatientResponseDto?> GetPatientByIdAsync(int id)
    {
        var patient = await _db.Patients.FindAsync(id);
        return patient == null ? null : MapToDto(patient);
    }

    // Helper Method: Entity to DTO Mapping
    private static PatientResponseDto MapToDto(Patient p) => new()
    {
        PatientId = p.PatientId,
        UHID = p.UHID,
        PatientName = p.PatientName,
        Phone = p.Phone,
        Gender = p.Gender,
        Age = p.Age,
        BloodGroup = p.BloodGroup,
        Address = p.Address,
        RegisteredAt = p.RegisteredAt
    };
}