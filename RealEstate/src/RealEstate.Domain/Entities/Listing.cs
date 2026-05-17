using RealEstate.Domain.Enums;

namespace RealEstate.Domain.Entities;

public class Listing : BaseEntity
{
    public int PropertyId { get; set; }
    public Property Property { get; set; } = null!;
    public int AgentId { get; set; }
    public Agent Agent { get; set; } = null!;
    public decimal Price { get; set; }
    public ListingType ListingType { get; set; }
    public ListingStatus Status { get; set; } = ListingStatus.Active;
    public DateTime ListedAt { get; set; } = DateTime.UtcNow;
    public DateTime? SoldOrRentedAt { get; set; }
    public ICollection<Inquiry> Inquiries { get; set; } = new List<Inquiry>();
}
