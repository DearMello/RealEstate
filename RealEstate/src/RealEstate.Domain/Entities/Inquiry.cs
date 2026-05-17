namespace RealEstate.Domain.Entities;

public class Inquiry : BaseEntity
{
    public int ListingId { get; set; }
    public Listing Listing { get; set; } = null!;
    public int ClientId { get; set; }
    public Client Client { get; set; } = null!;
    public string Message { get; set; } = string.Empty;
    public bool IsResponded { get; set; } = false;
    public string? Response { get; set; }
    public DateTime? RespondedAt { get; set; }
}
