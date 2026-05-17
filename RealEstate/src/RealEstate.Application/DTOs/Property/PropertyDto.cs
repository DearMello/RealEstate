using RealEstate.Domain.Enums;

namespace RealEstate.Application.DTOs.Property;

public class PropertyDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public double AreaSqm { get; set; }
    public int? Bedrooms { get; set; }
    public int? Bathrooms { get; set; }
    public int? FloorNumber { get; set; }
    public int? TotalFloors { get; set; }
    public int YearBuilt { get; set; }
    public PropertyType PropertyType { get; set; }
    public bool HasParking { get; set; }
    public bool HasGarden { get; set; }
}

public class CreatePropertyDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public double AreaSqm { get; set; }
    public int? Bedrooms { get; set; }
    public int? Bathrooms { get; set; }
    public int? FloorNumber { get; set; }
    public int? TotalFloors { get; set; }
    public int YearBuilt { get; set; }
    public PropertyType PropertyType { get; set; }
    public bool HasParking { get; set; }
    public bool HasGarden { get; set; }
}

public class UpdatePropertyDto : CreatePropertyDto { }
