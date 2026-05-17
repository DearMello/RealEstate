using RealEstate.Application.Common;
using RealEstate.Application.DTOs;
using RealEstate.Application.DTOs.Agent;
using RealEstate.Application.DTOs.Client;
using RealEstate.Application.DTOs.Listing;
using RealEstate.Application.DTOs.Property;
using RealEstate.Domain.Enums;

namespace RealEstate.Application.Interfaces;

public interface IPropertyService
{
    Task<Result<IEnumerable<PropertyDto>>> GetAllAsync();
    Task<Result<PropertyDto>> GetByIdAsync(int id);
    Task<Result<IEnumerable<PropertyDto>>> SearchAsync(string? city, PropertyType? type, double? minArea, double? maxArea);
    Task<Result<PropertyDto>> CreateAsync(CreatePropertyDto dto);
    Task<Result<PropertyDto>> UpdateAsync(int id, UpdatePropertyDto dto);
    Task<Result> DeleteAsync(int id);
}

public interface IAgentService
{
    Task<Result<IEnumerable<AgentDto>>> GetAllAsync();
    Task<Result<AgentDto>> GetByIdAsync(int id);
    Task<Result<AgentDto>> CreateAsync(CreateAgentDto dto);
    Task<Result<AgentDto>> UpdateAsync(int id, UpdateAgentDto dto);
    Task<Result> DeleteAsync(int id);
}

public interface IClientService
{
    Task<Result<IEnumerable<ClientDto>>> GetAllAsync();
    Task<Result<ClientDto>> GetByIdAsync(int id);
    Task<Result<ClientDto>> CreateAsync(CreateClientDto dto);
    Task<Result<ClientDto>> UpdateAsync(int id, UpdateClientDto dto);
}

public interface IListingService
{
    Task<Result<IEnumerable<ListingDto>>> GetAllAsync();
    Task<Result<ListingDto>> GetByIdAsync(int id);
    Task<Result<IEnumerable<ListingDto>>> GetByAgentAsync(int agentId);
    Task<Result<IEnumerable<ListingDto>>> SearchAsync(ListingSearchDto searchDto);
    Task<Result<ListingDto>> CreateAsync(CreateListingDto dto);
    Task<Result<ListingDto>> UpdateAsync(int id, UpdateListingDto dto);
    Task<Result<InquiryDto>> CreateInquiryAsync(CreateInquiryDto dto);
    Task<Result<InquiryDto>> RespondToInquiryAsync(int inquiryId, RespondInquiryDto dto);
    Task<Result<IEnumerable<InquiryDto>>> GetInquiriesByListingAsync(int listingId);
}

public interface IAuthService
{
    Task<Result<AuthResponseDto>> LoginAsync(LoginDto dto);
    Task<Result<AuthResponseDto>> RegisterAsync(RegisterDto dto);
}
