using RealEstate.Application.Common;
using RealEstate.Application.DTOs.Client;
using RealEstate.Application.Interfaces;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepo;

    public ClientService(IClientRepository clientRepo)
    {
        _clientRepo = clientRepo;
    }

    public async Task<Result<IEnumerable<ClientDto>>> GetAllAsync()
    {
        var clients = await _clientRepo.GetAllAsync();
        return Result<IEnumerable<ClientDto>>.Success(clients.Select(MapToDto));
    }

    public async Task<Result<ClientDto>> GetByIdAsync(int id)
    {
        var client = await _clientRepo.GetByIdAsync(id);
        if (client is null)
            return Result<ClientDto>.Failure($"Client with id {id} not found.");
        return Result<ClientDto>.Success(MapToDto(client));
    }

    public async Task<Result<ClientDto>> CreateAsync(CreateClientDto dto)
    {
        var existing = await _clientRepo.GetByEmailAsync(dto.Email);
        if (existing is not null)
            return Result<ClientDto>.Failure("A client with this email already exists.");

        var client = new Client
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Phone = dto.Phone,
            Notes = dto.Notes
        };

        var created = await _clientRepo.AddAsync(client);
        return Result<ClientDto>.Success(MapToDto(created));
    }

    public async Task<Result<ClientDto>> UpdateAsync(int id, UpdateClientDto dto)
    {
        var client = await _clientRepo.GetByIdAsync(id);
        if (client is null)
            return Result<ClientDto>.Failure($"Client with id {id} not found.");

        client.FirstName = dto.FirstName;
        client.LastName = dto.LastName;
        client.Email = dto.Email;
        client.Phone = dto.Phone;
        client.Notes = dto.Notes;
        client.UpdatedAt = DateTime.UtcNow;

        await _clientRepo.UpdateAsync(client);
        return Result<ClientDto>.Success(MapToDto(client));
    }

    private static ClientDto MapToDto(Client c) => new()
    {
        Id = c.Id,
        FirstName = c.FirstName,
        LastName = c.LastName,
        Email = c.Email,
        Phone = c.Phone,
        Notes = c.Notes
    };
}
