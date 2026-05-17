namespace RealEstate.Domain.Entities;

public class Client : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public ICollection<Inquiry> Inquiries { get; set; } = new List<Inquiry>();
}
