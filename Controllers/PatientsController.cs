using HospitalERP.DTOs;
using HospitalERP.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalERP.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] // 👈 Koi bhi logged-in user dekh sakta hai
public class PatientsController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientsController(IPatientService patientService) => _patientService = patientService;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var patients = await _patientService.GetAllPatientsAsync();
        return Ok(patients);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var patient = await _patientService.GetPatientByIdAsync(id);
        if (patient == null) return NotFound(new { message = "Patient not found" });
        return Ok(patient);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Receptionist")] // 👈 Sirf yeh log register kar sakte hain
    public async Task<IActionResult> Register([FromBody] CreatePatientDto dto)
    {
        try
        {
            var patient = await _patientService.RegisterPatientAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = patient.PatientId }, patient);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}