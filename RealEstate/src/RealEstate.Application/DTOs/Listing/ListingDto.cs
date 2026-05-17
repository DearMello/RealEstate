using RealEstate.Domain.Enums;

namespace RealEstate.Application.DTOs.Listing;

public class ListingDto
{
    public int Id { get; set; }
    public int PropertyId { get; set; }
    public string PropertyTitle { get; set; } = string.Empty;
    public string PropertyCity { get; set; } = string.Empty;
    public int AgentId { get; set; }
    public string AgentName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public ListingType ListingType { get; set; }
    public ListingStatus Status { get; set; }
    public DateTime ListedAt { get; set; }
    public DateTime? SoldOrRentedAt { get; set; }
}

public class CreateListingDto
{
    public int PropertyId { get; set; }
    public int AgentId { get; set; }
    public decimal Price { get; set; }
    public ListingType ListingType { get; set; }
}

public class UpdateListingDto
{
    public decimal Price { get; set; }
    public ListingStatus Status { get; set; }
}

public class ListingSearchDto
{
    public string? City { get; set; }
    public ListingType? ListingType { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
}

public class InquiryDto
{
    public int Id { get; set; }
    public int ListingId { get; set; }
    public int ClientId { get; set; }
    public string ClientName { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public bool IsResponded { get; set; }
    public string? Response { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateInquiryDto
{
    public int ListingId { get; set; }
    public int ClientId { get; set; }
    public string Message { get; set; } = string.Empty;
}

public class RespondInquiryDto
{
    public string Response { get; set; } = string.Empty;
}
