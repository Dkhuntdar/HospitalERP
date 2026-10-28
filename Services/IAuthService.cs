using HospitalERP.DTOs;

namespace HospitalERP.Services;

public interface IAuthService
{
    Task<string> RegisterAsync(RegisterRequestDto dto);
    Task<AuthResponseDto> LoginAsync(LoginRequestDto dto);
}