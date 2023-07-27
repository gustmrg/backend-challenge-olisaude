using Microsoft.AspNetCore.Mvc;
using OliSaude.API.Models;

namespace OliSaude.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PatientsController : ControllerBase
{
    [HttpGet]
    public ActionResult GetPatientsAsync()
    {
        return Ok();
    }
    
    [HttpGet]
    [Route("{id:guid}")]
    public ActionResult GetPatientByIdAsync(Guid id)
    {
        return Ok();
    }
    
    [HttpPost]
    public ActionResult CreatePatientAsync(Patient patient)
    {
        return Ok();
    }

    [HttpPut]
    [Route("{id:guid}")]
    public ActionResult UpdatePatientByIdAsync(
        [FromRoute] Guid id, 
        [FromBody] Patient patient)
    {
        return Ok();
    }
}