using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OliSaude.API.Data;
using OliSaude.API.Models;

namespace OliSaude.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PatientsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PatientsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult> GetPatientsAsync()
    {
        try
        {
            var patients = await _context.Patients.AsNoTracking().ToListAsync();

            return Ok(patients);
        }
        catch (Exception)
        {
            return BadRequest(new { message = "Could not retrieve list of patients." });
        }
    }
    
    [HttpGet]
    [Route("{id:guid}")]
    public async Task<ActionResult> GetPatientByIdAsync(Guid id)
    {
        try
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.Id == id);

            if (patient == null)
                return NotFound();

            return Ok(patient);
        }
        catch (Exception)
        {
            return BadRequest(new { message = "Could not find patient." });
        }
    }
    
    [HttpPost]
    public async Task<ActionResult> CreatePatientAsync(PatientDTO patientDTO)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        try
        {
            var patient = new Patient
            {
                Id = Guid.NewGuid(),
                Name = patientDTO.Name,
                Gender = patientDTO.Gender,
                DateOfBirth = patientDTO.DateOfBirth,
                CreatedDate = DateTime.Now
            };
            
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
            
            return Ok(patient);
        }
        catch (Exception)
        {
            return BadRequest(new { message = "Could not create patient." });
        }
    }

    [HttpPut]
    [Route("{id:guid}")]
    public async Task<ActionResult> UpdatePatientByIdAsync(
        [FromRoute] Guid id, 
        [FromBody] PatientDTO patientDTO)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        try
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.Id == id);
            
            if (patient == null)
                return NotFound();

            patient.Name = patientDTO.Name;
            patient.Gender = patientDTO.Gender;
            patient.UpdatedDate = DateTime.Now;

            _context.Patients.Update(patient);
            await _context.SaveChangesAsync();

            return Ok(patient);
        }
        catch (Exception)
        {
            return BadRequest(new { message = "Could not create patient." });
        }
    }
}