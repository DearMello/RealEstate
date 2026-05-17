using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.DTOs.Agent;
using RealEstate.Application.Interfaces;

namespace RealEstate.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AgentsController : ControllerBase
{
    private readonly IAgentService _agentService;
    public AgentsController(IAgentService agentService) => _agentService = agentService;

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok((await _agentService.GetAllAsync()).Data);

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _agentService.GetByIdAsync(id);
        if (!result.IsSuccess) return NotFound(new { message = result.Error });
        return Ok(result.Data);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateAgentDto dto)
    {
        var result = await _agentService.CreateAsync(dto);
        if (!result.IsSuccess) return BadRequest(new { message = result.Error });
        return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result.Data);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateAgentDto dto)
    {
        var result = await _agentService.UpdateAsync(id, dto);
        if (!result.IsSuccess) return BadRequest(new { message = result.Error });
        return Ok(result.Data);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _agentService.DeleteAsync(id);
        if (!result.IsSuccess) return NotFound(new { message = result.Error });
        return NoContent();
    }
}
