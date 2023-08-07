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

    /// <summary>
    /// Pacientes cadastrados no banco de dados.
    /// </summary>
    /// <returns>Lista de pacientes cadastrados.</returns>
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
    
    /// <summary>
    /// Pacientes com maior risco de saúde de acordo com fórmula de cálculo fornecida.
    /// </summary>
    /// <returns>Lista de 10 pacientes cadastrados com maior risco de saúde.</returns>
    [HttpGet("greater-risk")]
    public async Task<ActionResult> GetPatientsWithGreaterRiskAsync()
    {
        // TODO: Add validation to return only patients with greater health risk
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
    
    /// <summary>
    /// Busca no banco de dados pelo id o paciente cadastrado.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Cadastra um novo paciente no banco de dados.
    /// </summary>
    /// <param name="patientDTO"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Atualiza as informações de cadastro do paciente informado.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="patientDTO"></param>
    /// <returns></returns>
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

    public static void GetTotalRisk(int[] severity)
    {
        throw new NotImplementedException();
    }

    public static double GetScore(int totalRisk)
    {
        var e = 2.71828d;

        var score = (1 / (1 + Math.Pow(e, -(-2.8 + totalRisk)))) * 100;

        return score;
    }
}

