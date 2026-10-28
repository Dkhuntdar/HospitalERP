using HospitalERP.Data;
using HospitalERP.DTOs;
using HospitalERP.Entities;
using Microsoft.EntityFrameworkCore;

namespace HospitalERP.Services;

public class AppointmentService : IAppointmentService
{
    private readonly AppDbContext _db;

    public AppointmentService(AppDbContext db) => _db = db;

    public async Task<IEnumerable<AppointmentResponseDto>> GetAllAppointmentsAsync()
    {
        return await _db.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .OrderByDescending(a => a.AppointmentDate)
            .Select(a => new AppointmentResponseDto
            {
                AppointmentId = a.AppointmentId,
                PatientName = a.Patient.PatientName,
                DoctorName = a.Doctor.DoctorName,
                AppointmentDate = a.AppointmentDate,
                Status = a.Status,
                Notes = a.Notes
            })
            .ToListAsync();
    }

    public async Task<AppointmentResponseDto> BookAppointmentAsync(CreateAppointmentDto dto)
    {
        // Rule 1: Date past ki nahi honi chahiye
        if (dto.AppointmentDate <= DateTime.UtcNow)
            throw new InvalidOperationException("Appointment date must be in the future.");

        // Rule 2: Doctor aur Patient exist karte hain?
        var patient = await _db.Patients.FindAsync(dto.PatientId) ?? throw new InvalidOperationException("Patient not found.");
        var doctor = await _db.Doctors.FindAsync(dto.DoctorId) ?? throw new InvalidOperationException("Doctor not found.");

        // Rule 3: Kya us doctor ka same time pe pehle se appointment hai?
        var conflict = await _db.Appointments.AnyAsync(a =>
            a.DoctorId == dto.DoctorId &&
            a.AppointmentDate == dto.AppointmentDate &&
            a.Status == "Scheduled");

        if (conflict)
            throw new InvalidOperationException("This doctor is already booked at this time.");

        var appointment = new Appointment
        {
            PatientId = dto.PatientId,
            DoctorId = dto.DoctorId,
            AppointmentDate = dto.AppointmentDate,
            Notes = dto.Notes,
            Status = "Scheduled"
        };

        _db.Appointments.Add(appointment);
        await _db.SaveChangesAsync();

        return new AppointmentResponseDto
        {
            AppointmentId = appointment.AppointmentId,
            PatientName = patient.PatientName,
            DoctorName = doctor.DoctorName,
            AppointmentDate = appointment.AppointmentDate,
            Status = appointment.Status,
            Notes = appointment.Notes
        };
    }

    public async Task<bool> CancelAppointmentAsync(int id)
    {
        var appt = await _db.Appointments.FindAsync(id);
        if (appt == null) return false;

        if (appt.Status == "Completed")
            throw new InvalidOperationException("Cannot cancel a completed appointment.");

        appt.Status = "Cancelled";
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> CompleteAppointmentAsync(int id)
    {
        var appt = await _db.Appointments.FindAsync(id);
        if (appt == null) return false;

        if (appt.Status == "Cancelled")
            throw new InvalidOperationException("Cannot complete a cancelled appointment.");

        appt.Status = "Completed";
        await _db.SaveChangesAsync();
        return true;
    }
}