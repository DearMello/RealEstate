using RealEstate.Domain.Entities;
using RealEstate.Domain.Enums;

namespace RealEstate.Domain.Interfaces;

public interface IPropertyRepository
{
    Task<IEnumerable<Property>> GetAllAsync();
    Task<Property?> GetByIdAsync(int id);
    Task<IEnumerable<Property>> SearchAsync(string? city, PropertyType? type, double? minArea, double? maxArea);
    Task<Property> AddAsync(Property property);
    Task UpdateAsync(Property property);
    Task DeleteAsync(int id);
}

public interface IAgentRepository
{
    Task<IEnumerable<Agent>> GetAllAsync();
    Task<Agent?> GetByIdAsync(int id);
    Task<Agent?> GetByEmailAsync(string email);
    Task<Agent> AddAsync(Agent agent);
    Task UpdateAsync(Agent agent);
    Task DeleteAsync(int id);
}

public interface IClientRepository
{
    Task<IEnumerable<Client>> GetAllAsync();
    Task<Client?> GetByIdAsync(int id);
    Task<Client?> GetByEmailAsync(string email);
    Task<Client> AddAsync(Client client);
    Task UpdateAsync(Client client);
}

public interface IListingRepository
{
    Task<IEnumerable<Listing>> GetAllAsync();
    Task<Listing?> GetByIdAsync(int id);
    Task<IEnumerable<Listing>> GetByAgentAsync(int agentId);
    Task<IEnumerable<Listing>> GetByStatusAsync(ListingStatus status);
    Task<IEnumerable<Listing>> SearchAsync(string? city, ListingType? type, decimal? minPrice, decimal? maxPrice);
    Task<Listing> AddAsync(Listing listing);
    Task UpdateAsync(Listing listing);
}

public interface IInquiryRepository
{
    Task<IEnumerable<Inquiry>> GetByListingAsync(int listingId);
    Task<IEnumerable<Inquiry>> GetByClientAsync(int clientId);
    Task<Inquiry?> GetByIdAsync(int id);
    Task<Inquiry> AddAsync(Inquiry inquiry);
    Task UpdateAsync(Inquiry inquiry);
}

public interface IUserRepository
{
    Task<AppUser?> GetByEmailAsync(string email);
    Task<AppUser> AddAsync(AppUser user);
}
