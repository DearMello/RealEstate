using RealEstate.Application.Common;
using RealEstate.Application.DTOs.Property;
using RealEstate.Application.Interfaces;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Enums;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Services;

public class PropertyService : IPropertyService
{
    private readonly IPropertyRepository _propertyRepo;

    public PropertyService(IPropertyRepository propertyRepo)
    {
        _propertyRepo = propertyRepo;
    }

    public async Task<Result<IEnumerable<PropertyDto>>> GetAllAsync()
    {
        var properties = await _propertyRepo.GetAllAsync();
        return Result<IEnumerable<PropertyDto>>.Success(properties.Select(MapToDto));
    }

    public async Task<Result<PropertyDto>> GetByIdAsync(int id)
    {
        var property = await _propertyRepo.GetByIdAsync(id);
        if (property is null)
            return Result<PropertyDto>.Failure($"Property with id {id} not found.");
        return Result<PropertyDto>.Success(MapToDto(property));
    }

    public async Task<Result<IEnumerable<PropertyDto>>> SearchAsync(string? city, PropertyType? type, double? minArea, double? maxArea)
    {
        var properties = await _propertyRepo.SearchAsync(city, type, minArea, maxArea);
        return Result<IEnumerable<PropertyDto>>.Success(properties.Select(MapToDto));
    }

    public async Task<Result<PropertyDto>> CreateAsync(CreatePropertyDto dto)
    {
        var property = new Property
        {
            Title = dto.Title,
            Description = dto.Description,
            Address = dto.Address,
            City = dto.City,
            Country = dto.Country,
            AreaSqm = dto.AreaSqm,
            Bedrooms = dto.Bedrooms,
            Bathrooms = dto.Bathrooms,
            FloorNumber = dto.FloorNumber,
            TotalFloors = dto.TotalFloors,
            YearBuilt = dto.YearBuilt,
            PropertyType = dto.PropertyType,
            HasParking = dto.HasParking,
            HasGarden = dto.HasGarden
        };

        var created = await _propertyRepo.AddAsync(property);
        return Result<PropertyDto>.Success(MapToDto(created));
    }

    public async Task<Result<PropertyDto>> UpdateAsync(int id, UpdatePropertyDto dto)
    {
        var property = await _propertyRepo.GetByIdAsync(id);
        if (property is null)
            return Result<PropertyDto>.Failure($"Property with id {id} not found.");

        property.Title = dto.Title;
        property.Description = dto.Description;
        property.Address = dto.Address;
        property.City = dto.City;
        property.Country = dto.Country;
        property.AreaSqm = dto.AreaSqm;
        property.Bedrooms = dto.Bedrooms;
        property.Bathrooms = dto.Bathrooms;
        property.FloorNumber = dto.FloorNumber;
        property.TotalFloors = dto.TotalFloors;
        property.YearBuilt = dto.YearBuilt;
        property.PropertyType = dto.PropertyType;
        property.HasParking = dto.HasParking;
        property.HasGarden = dto.HasGarden;
        property.UpdatedAt = DateTime.UtcNow;

        await _propertyRepo.UpdateAsync(property);
        return Result<PropertyDto>.Success(MapToDto(property));
    }

    public async Task<Result> DeleteAsync(int id)
    {
        var property = await _propertyRepo.GetByIdAsync(id);
        if (property is null)
            return Result.Failure($"Property with id {id} not found.");
        await _propertyRepo.DeleteAsync(id);
        return Result.Success();
    }

    private static PropertyDto MapToDto(Property p) => new()
    {
        Id = p.Id,
        Title = p.Title,
        Description = p.Description,
        Address = p.Address,
        City = p.City,
        Country = p.Country,
        AreaSqm = p.AreaSqm,
        Bedrooms = p.Bedrooms,
        Bathrooms = p.Bathrooms,
        FloorNumber = p.FloorNumber,
        TotalFloors = p.TotalFloors,
        YearBuilt = p.YearBuilt,
        PropertyType = p.PropertyType,
        HasParking = p.HasParking,
        HasGarden = p.HasGarden
    };
}
