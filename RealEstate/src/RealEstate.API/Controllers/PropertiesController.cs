using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.DTOs.Property;
using RealEstate.Application.Interfaces;
using RealEstate.Domain.Enums;

namespace RealEstate.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PropertiesController : ControllerBase
{
    private readonly IPropertyService _propertyService;
    public PropertiesController(IPropertyService propertyService) => _propertyService = propertyService;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _propertyService.GetAllAsync();
        return Ok(result.Data);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _propertyService.GetByIdAsync(id);
        if (!result.IsSuccess) return NotFound(new { message = result.Error });
        return Ok(result.Data);
    }

    [HttpGet("search")]
    [AllowAnonymous]
    public async Task<IActionResult> Search([FromQuery] string? city, [FromQuery] PropertyType? type,
        [FromQuery] double? minArea, [FromQuery] double? maxArea)
    {
        var result = await _propertyService.SearchAsync(city, type, minArea, maxArea);
        return Ok(result.Data);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePropertyDto dto)
    {
        var result = await _propertyService.CreateAsync(dto);
        if (!result.IsSuccess) return BadRequest(new { message = result.Error });
        return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result.Data);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePropertyDto dto)
    {
        var result = await _propertyService.UpdateAsync(id, dto);
        if (!result.IsSuccess) return BadRequest(new { message = result.Error });
        return Ok(result.Data);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _propertyService.DeleteAsync(id);
        if (!result.IsSuccess) return NotFound(new { message = result.Error });
        return NoContent();
    }
}
