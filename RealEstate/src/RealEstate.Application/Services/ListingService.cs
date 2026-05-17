using RealEstate.Application.Common;
using RealEstate.Application.DTOs.Listing;
using RealEstate.Application.Interfaces;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Enums;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Services;

public class ListingService : IListingService
{
    private readonly IListingRepository _listingRepo;
    private readonly IPropertyRepository _propertyRepo;
    private readonly IAgentRepository _agentRepo;
    private readonly IClientRepository _clientRepo;
    private readonly IInquiryRepository _inquiryRepo;

    public ListingService(IListingRepository listingRepo, IPropertyRepository propertyRepo,
        IAgentRepository agentRepo, IClientRepository clientRepo, IInquiryRepository inquiryRepo)
    {
        _listingRepo = listingRepo;
        _propertyRepo = propertyRepo;
        _agentRepo = agentRepo;
        _clientRepo = clientRepo;
        _inquiryRepo = inquiryRepo;
    }

    public async Task<Result<IEnumerable<ListingDto>>> GetAllAsync()
    {
        var listings = await _listingRepo.GetAllAsync();
        return Result<IEnumerable<ListingDto>>.Success(listings.Select(MapToDto));
    }

    public async Task<Result<ListingDto>> GetByIdAsync(int id)
    {
        var listing = await _listingRepo.GetByIdAsync(id);
        if (listing is null)
            return Result<ListingDto>.Failure($"Listing with id {id} not found.");
        return Result<ListingDto>.Success(MapToDto(listing));
    }

    public async Task<Result<IEnumerable<ListingDto>>> GetByAgentAsync(int agentId)
    {
        var listings = await _listingRepo.GetByAgentAsync(agentId);
        return Result<IEnumerable<ListingDto>>.Success(listings.Select(MapToDto));
    }

    public async Task<Result<IEnumerable<ListingDto>>> SearchAsync(ListingSearchDto searchDto)
    {
        var listings = await _listingRepo.SearchAsync(searchDto.City, searchDto.ListingType, searchDto.MinPrice, searchDto.MaxPrice);
        return Result<IEnumerable<ListingDto>>.Success(listings.Select(MapToDto));
    }

    public async Task<Result<ListingDto>> CreateAsync(CreateListingDto dto)
    {
        var property = await _propertyRepo.GetByIdAsync(dto.PropertyId);
        if (property is null)
            return Result<ListingDto>.Failure("Property not found.");

        var agent = await _agentRepo.GetByIdAsync(dto.AgentId);
        if (agent is null)
            return Result<ListingDto>.Failure("Agent not found.");

        var listing = new Listing
        {
            PropertyId = dto.PropertyId,
            AgentId = dto.AgentId,
            Price = dto.Price,
            ListingType = dto.ListingType,
            Status = ListingStatus.Active,
            ListedAt = DateTime.UtcNow
        };

        var created = await _listingRepo.AddAsync(listing);
        created.Property = property;
        created.Agent = agent;
        return Result<ListingDto>.Success(MapToDto(created));
    }

    public async Task<Result<ListingDto>> UpdateAsync(int id, UpdateListingDto dto)
    {
        var listing = await _listingRepo.GetByIdAsync(id);
        if (listing is null)
            return Result<ListingDto>.Failure($"Listing with id {id} not found.");

        listing.Price = dto.Price;
        listing.Status = dto.Status;
        if (dto.Status == ListingStatus.Sold || dto.Status == ListingStatus.Rented)
            listing.SoldOrRentedAt = DateTime.UtcNow;
        listing.UpdatedAt = DateTime.UtcNow;

        await _listingRepo.UpdateAsync(listing);
        return Result<ListingDto>.Success(MapToDto(listing));
    }

    public async Task<Result<InquiryDto>> CreateInquiryAsync(CreateInquiryDto dto)
    {
        var listing = await _listingRepo.GetByIdAsync(dto.ListingId);
        if (listing is null)
            return Result<InquiryDto>.Failure("Listing not found.");

        var client = await _clientRepo.GetByIdAsync(dto.ClientId);
        if (client is null)
            return Result<InquiryDto>.Failure("Client not found.");

        var inquiry = new Inquiry
        {
            ListingId = dto.ListingId,
            ClientId = dto.ClientId,
            Message = dto.Message
        };

        var created = await _inquiryRepo.AddAsync(inquiry);
        created.Client = client;
        return Result<InquiryDto>.Success(MapInquiryToDto(created));
    }

    public async Task<Result<InquiryDto>> RespondToInquiryAsync(int inquiryId, RespondInquiryDto dto)
    {
        var inquiry = await _inquiryRepo.GetByIdAsync(inquiryId);
        if (inquiry is null)
            return Result<InquiryDto>.Failure("Inquiry not found.");

        inquiry.Response = dto.Response;
        inquiry.IsResponded = true;
        inquiry.RespondedAt = DateTime.UtcNow;
        inquiry.UpdatedAt = DateTime.UtcNow;

        await _inquiryRepo.UpdateAsync(inquiry);
        return Result<InquiryDto>.Success(MapInquiryToDto(inquiry));
    }

    public async Task<Result<IEnumerable<InquiryDto>>> GetInquiriesByListingAsync(int listingId)
    {
        var inquiries = await _inquiryRepo.GetByListingAsync(listingId);
        return Result<IEnumerable<InquiryDto>>.Success(inquiries.Select(MapInquiryToDto));
    }

    private static ListingDto MapToDto(Listing l) => new()
    {
        Id = l.Id,
        PropertyId = l.PropertyId,
        PropertyTitle = l.Property?.Title ?? string.Empty,
        PropertyCity = l.Property?.City ?? string.Empty,
        AgentId = l.AgentId,
        AgentName = l.Agent is not null ? $"{l.Agent.FirstName} {l.Agent.LastName}" : string.Empty,
        Price = l.Price,
        ListingType = l.ListingType,
        Status = l.Status,
        ListedAt = l.ListedAt,
        SoldOrRentedAt = l.SoldOrRentedAt
    };

    private static InquiryDto MapInquiryToDto(Inquiry i) => new()
    {
        Id = i.Id,
        ListingId = i.ListingId,
        ClientId = i.ClientId,
        ClientName = i.Client is not null ? $"{i.Client.FirstName} {i.Client.LastName}" : string.Empty,
        Message = i.Message,
        IsResponded = i.IsResponded,
        Response = i.Response,
        CreatedAt = i.CreatedAt
    };
}
