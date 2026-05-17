namespace RealEstate.Application.DTOs.Agent;

public class AgentDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName => $"{FirstName} {LastName}";
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? Bio { get; set; }
    public string LicenseNumber { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public int ListingCount { get; set; }
}

public class CreateAgentDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? Bio { get; set; }
    public string LicenseNumber { get; set; } = string.Empty;
}

public class UpdateAgentDto : CreateAgentDto
{
    public bool IsActive { get; set; } = true;
}
