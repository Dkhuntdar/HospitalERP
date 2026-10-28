using HospitalERP.DTOs;
using HospitalERP.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalERP.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DoctorsController : ControllerBase
{
    private readonly IDoctorService _doctorService;

    public DoctorsController(IDoctorService doctorService) => _doctorService = doctorService;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var doctors = await _doctorService.GetAllDoctorsAsync();
        return Ok(doctors);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")] // 👈 Sirf Admin hi Doctor bana sakta hai
    public async Task<IActionResult> Create([FromBody] CreateDoctorDto dto)
    {
        try
        {
            var doctor = await _doctorService.CreateDoctorAsync(dto);
            return Ok(doctor);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}