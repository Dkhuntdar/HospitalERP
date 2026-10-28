using HospitalERP.DTOs;

namespace HospitalERP.Services;

public interface IAppointmentService
{
    Task<IEnumerable<AppointmentResponseDto>> GetAllAppointmentsAsync();
    Task<AppointmentResponseDto> BookAppointmentAsync(CreateAppointmentDto dto);
    Task<bool> CancelAppointmentAsync(int id);
    Task<bool> CompleteAppointmentAsync(int id);
}