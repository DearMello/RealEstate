using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.DTOs.Listing;
using RealEstate.Application.Interfaces;

namespace RealEstate.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ListingsController : ControllerBase
{
    private readonly IListingService _listingService;
    public ListingsController(IListingService listingService) => _listingService = listingService;

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok((await _listingService.GetAllAsync()).Data);

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _listingService.GetByIdAsync(id);
        if (!result.IsSuccess) return NotFound(new { message = result.Error });
        return Ok(result.Data);
    }

    [HttpGet("agent/{agentId}")]
    public async Task<IActionResult> GetByAgent(int agentId)
        => Ok((await _listingService.GetByAgentAsync(agentId)).Data);

    [HttpGet("search")]
    [AllowAnonymous]
    public async Task<IActionResult> Search([FromQuery] ListingSearchDto searchDto)
    {
        var result = await _listingService.SearchAsync(searchDto);
        return Ok(result.Data);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateListingDto dto)
    {
        var result = await _listingService.CreateAsync(dto);
        if (!result.IsSuccess) return BadRequest(new { message = result.Error });
        return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result.Data);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateListingDto dto)
    {
        var result = await _listingService.UpdateAsync(id, dto);
        if (!result.IsSuccess) return BadRequest(new { message = result.Error });
        return Ok(result.Data);
    }

    [HttpPost("inquiries")]
    [AllowAnonymous]
    public async Task<IActionResult> CreateInquiry([FromBody] CreateInquiryDto dto)
    {
        var result = await _listingService.CreateInquiryAsync(dto);
        if (!result.IsSuccess) return BadRequest(new { message = result.Error });
        return Ok(result.Data);
    }

    [HttpPut("inquiries/{inquiryId}/respond")]
    public async Task<IActionResult> RespondToInquiry(int inquiryId, [FromBody] RespondInquiryDto dto)
    {
        var result = await _listingService.RespondToInquiryAsync(inquiryId, dto);
        if (!result.IsSuccess) return BadRequest(new { message = result.Error });
        return Ok(result.Data);
    }

    [HttpGet("{id}/inquiries")]
    public async Task<IActionResult> GetInquiries(int id)
        => Ok((await _listingService.GetInquiriesByListingAsync(id)).Data);
}
