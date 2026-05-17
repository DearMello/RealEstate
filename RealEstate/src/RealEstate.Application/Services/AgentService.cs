using RealEstate.Application.Common;
using RealEstate.Application.DTOs.Agent;
using RealEstate.Application.Interfaces;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Services;

public class AgentService : IAgentService
{
    private readonly IAgentRepository _agentRepo;

    public AgentService(IAgentRepository agentRepo)
    {
        _agentRepo = agentRepo;
    }

    public async Task<Result<IEnumerable<AgentDto>>> GetAllAsync()
    {
        var agents = await _agentRepo.GetAllAsync();
        return Result<IEnumerable<AgentDto>>.Success(agents.Select(MapToDto));
    }

    public async Task<Result<AgentDto>> GetByIdAsync(int id)
    {
        var agent = await _agentRepo.GetByIdAsync(id);
        if (agent is null)
            return Result<AgentDto>.Failure($"Agent with id {id} not found.");
        return Result<AgentDto>.Success(MapToDto(agent));
    }

    public async Task<Result<AgentDto>> CreateAsync(CreateAgentDto dto)
    {
        var existing = await _agentRepo.GetByEmailAsync(dto.Email);
        if (existing is not null)
            return Result<AgentDto>.Failure("An agent with this email already exists.");

        var agent = new Agent
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Phone = dto.Phone,
            Bio = dto.Bio,
            LicenseNumber = dto.LicenseNumber
        };

        var created = await _agentRepo.AddAsync(agent);
        return Result<AgentDto>.Success(MapToDto(created));
    }

    public async Task<Result<AgentDto>> UpdateAsync(int id, UpdateAgentDto dto)
    {
        var agent = await _agentRepo.GetByIdAsync(id);
        if (agent is null)
            return Result<AgentDto>.Failure($"Agent with id {id} not found.");

        agent.FirstName = dto.FirstName;
        agent.LastName = dto.LastName;
        agent.Email = dto.Email;
        agent.Phone = dto.Phone;
        agent.Bio = dto.Bio;
        agent.LicenseNumber = dto.LicenseNumber;
        agent.IsActive = dto.IsActive;
        agent.UpdatedAt = DateTime.UtcNow;

        await _agentRepo.UpdateAsync(agent);
        return Result<AgentDto>.Success(MapToDto(agent));
    }

    public async Task<Result> DeleteAsync(int id)
    {
        var agent = await _agentRepo.GetByIdAsync(id);
        if (agent is null)
            return Result.Failure($"Agent with id {id} not found.");
        await _agentRepo.DeleteAsync(id);
        return Result.Success();
    }

    private static AgentDto MapToDto(Agent a) => new()
    {
        Id = a.Id,
        FirstName = a.FirstName,
        LastName = a.LastName,
        Email = a.Email,
        Phone = a.Phone,
        Bio = a.Bio,
        LicenseNumber = a.LicenseNumber,
        IsActive = a.IsActive,
        ListingCount = a.Listings?.Count ?? 0
    };
}
