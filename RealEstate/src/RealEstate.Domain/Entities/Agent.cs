namespace RealEstate.Domain.Entities;

public class Agent : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? Bio { get; set; }
    public string LicenseNumber { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public ICollection<Listing> Listings { get; set; } = new List<Listing>();
}
