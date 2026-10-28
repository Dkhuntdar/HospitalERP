using HospitalERP.DTOs;

namespace HospitalERP.Services;

public interface IDoctorService
{
    Task<DoctorResponseDto> CreateDoctorAsync(CreateDoctorDto dto);
    Task<IEnumerable<DoctorResponseDto>> GetAllDoctorsAsync();
}