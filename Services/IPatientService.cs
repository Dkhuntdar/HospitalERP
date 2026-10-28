using HospitalERP.DTOs;

namespace HospitalERP.Services;

public interface IPatientService
{
    Task<PatientResponseDto> RegisterPatientAsync(CreatePatientDto dto);
    Task<IEnumerable<PatientResponseDto>> GetAllPatientsAsync();
    Task<PatientResponseDto?> GetPatientByIdAsync(int id);
}