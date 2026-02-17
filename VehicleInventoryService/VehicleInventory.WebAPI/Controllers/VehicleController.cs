using Microsoft.AspNetCore.Mvc;
using VehicleInventory.Application.DTOs;
using VehicleInventory.Application.Services;

namespace VehicleInventory.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VehiclesController : ControllerBase
{
    private readonly VehicleService _service;

    public VehiclesController(VehicleService service)
    {
        _service = service;
    }

    // GET /api/vehicles
    [HttpGet]
    public async Task<ActionResult<List<VehicleResponse>>> GetAll()
    {
        var vehicles = await _service.GetAllVehiclesAsync();
        return Ok(vehicles);
    }

    // GET /api/vehicles/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<VehicleResponse>> GetById(int id)
    {
        var vehicle = await _service.GetVehicleByIdAsync(id);
        return Ok(vehicle);
    }

    // POST /api/vehicles
    [HttpPost]
    public async Task<ActionResult<VehicleResponse>> Create([FromBody] CreateVehicleRequest request)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        var created = await _service.CreateVehicleAsync(request);

        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT /api/vehicles/{id}/status
    [HttpPut("{id:int}/status")]
    public async Task<ActionResult<VehicleResponse>> UpdateStatus(int id, [FromBody] UpdateVehicleStatusRequest request)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        var updated = await _service.UpdateVehicleStatusAsync(id, request);
        return Ok(updated);
    }

    // DELETE /api/vehicles/{id}
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteVehicleAsync(id);
        return NoContent();
    }
}