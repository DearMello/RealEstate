using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.DTOs.Client;
using RealEstate.Application.Interfaces;

namespace RealEstate.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ClientsController : ControllerBase
{
    private readonly IClientService _clientService;
    public ClientsController(IClientService clientService) => _clientService = clientService;

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok((await _clientService.GetAllAsync()).Data);

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _clientService.GetByIdAsync(id);
        if (!result.IsSuccess) return NotFound(new { message = result.Error });
        return Ok(result.Data);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateClientDto dto)
    {
        var result = await _clientService.CreateAsync(dto);
        if (!result.IsSuccess) return BadRequest(new { message = result.Error });
        return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result.Data);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateClientDto dto)
    {
        var result = await _clientService.UpdateAsync(id, dto);
        if (!result.IsSuccess) return BadRequest(new { message = result.Error });
        return Ok(result.Data);
    }
}
