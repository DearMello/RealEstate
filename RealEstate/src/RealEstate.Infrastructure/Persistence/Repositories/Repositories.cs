using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Enums;
using RealEstate.Domain.Interfaces;
using RealEstate.Infrastructure.Persistence;

namespace RealEstate.Infrastructure.Persistence.Repositories;

public class PropertyRepository : IPropertyRepository
{
    private readonly AppDbContext _context;
    public PropertyRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<Property>> GetAllAsync()
        => await _context.Properties.ToListAsync();

    public async Task<Property?> GetByIdAsync(int id)
        => await _context.Properties.Include(p => p.Listings).FirstOrDefaultAsync(p => p.Id == id);

    public async Task<IEnumerable<Property>> SearchAsync(string? city, PropertyType? type, double? minArea, double? maxArea)
    {
        var query = _context.Properties.AsQueryable();
        if (!string.IsNullOrEmpty(city)) query = query.Where(p => p.City.Contains(city));
        if (type.HasValue) query = query.Where(p => p.PropertyType == type.Value);
        if (minArea.HasValue) query = query.Where(p => p.AreaSqm >= minArea.Value);
        if (maxArea.HasValue) query = query.Where(p => p.AreaSqm <= maxArea.Value);
        return await query.ToListAsync();
    }

    public async Task<Property> AddAsync(Property property)
    {
        _context.Properties.Add(property);
        await _context.SaveChangesAsync();
        return property;
    }

    public async Task UpdateAsync(Property property)
    {
        _context.Properties.Update(property);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var p = await _context.Properties.FindAsync(id);
        if (p is not null) { _context.Properties.Remove(p); await _context.SaveChangesAsync(); }
    }
}

public class AgentRepository : IAgentRepository
{
    private readonly AppDbContext _context;
    public AgentRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<Agent>> GetAllAsync()
        => await _context.Agents.Include(a => a.Listings).ToListAsync();

    public async Task<Agent?> GetByIdAsync(int id)
        => await _context.Agents.Include(a => a.Listings).FirstOrDefaultAsync(a => a.Id == id);

    public async Task<Agent?> GetByEmailAsync(string email)
        => await _context.Agents.FirstOrDefaultAsync(a => a.Email == email);

    public async Task<Agent> AddAsync(Agent agent)
    {
        _context.Agents.Add(agent);
        await _context.SaveChangesAsync();
        return agent;
    }

    public async Task UpdateAsync(Agent agent)
    {
        _context.Agents.Update(agent);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var a = await _context.Agents.FindAsync(id);
        if (a is not null) { _context.Agents.Remove(a); await _context.SaveChangesAsync(); }
    }
}

public class ClientRepository : IClientRepository
{
    private readonly AppDbContext _context;
    public ClientRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<Client>> GetAllAsync()
        => await _context.Clients.ToListAsync();

    public async Task<Client?> GetByIdAsync(int id)
        => await _context.Clients.FirstOrDefaultAsync(c => c.Id == id);

    public async Task<Client?> GetByEmailAsync(string email)
        => await _context.Clients.FirstOrDefaultAsync(c => c.Email == email);

    public async Task<Client> AddAsync(Client client)
    {
        _context.Clients.Add(client);
        await _context.SaveChangesAsync();
        return client;
    }

    public async Task UpdateAsync(Client client)
    {
        _context.Clients.Update(client);
        await _context.SaveChangesAsync();
    }
}

public class ListingRepository : IListingRepository
{
    private readonly AppDbContext _context;
    public ListingRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<Listing>> GetAllAsync()
        => await _context.Listings.Include(l => l.Property).Include(l => l.Agent).ToListAsync();

    public async Task<Listing?> GetByIdAsync(int id)
        => await _context.Listings.Include(l => l.Property).Include(l => l.Agent)
            .Include(l => l.Inquiries).FirstOrDefaultAsync(l => l.Id == id);

    public async Task<IEnumerable<Listing>> GetByAgentAsync(int agentId)
        => await _context.Listings.Include(l => l.Property).Include(l => l.Agent)
            .Where(l => l.AgentId == agentId).ToListAsync();

    public async Task<IEnumerable<Listing>> GetByStatusAsync(ListingStatus status)
        => await _context.Listings.Include(l => l.Property).Include(l => l.Agent)
            .Where(l => l.Status == status).ToListAsync();

    public async Task<IEnumerable<Listing>> SearchAsync(string? city, ListingType? type, decimal? minPrice, decimal? maxPrice)
    {
        var query = _context.Listings.Include(l => l.Property).Include(l => l.Agent).AsQueryable();
        if (!string.IsNullOrEmpty(city)) query = query.Where(l => l.Property.City.Contains(city));
        if (type.HasValue) query = query.Where(l => l.ListingType == type.Value);
        if (minPrice.HasValue) query = query.Where(l => l.Price >= minPrice.Value);
        if (maxPrice.HasValue) query = query.Where(l => l.Price <= maxPrice.Value);
        return await query.ToListAsync();
    }

    public async Task<Listing> AddAsync(Listing listing)
    {
        _context.Listings.Add(listing);
        await _context.SaveChangesAsync();
        return listing;
    }

    public async Task UpdateAsync(Listing listing)
    {
        _context.Listings.Update(listing);
        await _context.SaveChangesAsync();
    }
}

public class InquiryRepository : IInquiryRepository
{
    private readonly AppDbContext _context;
    public InquiryRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<Inquiry>> GetByListingAsync(int listingId)
        => await _context.Inquiries.Include(i => i.Client)
            .Where(i => i.ListingId == listingId).ToListAsync();

    public async Task<IEnumerable<Inquiry>> GetByClientAsync(int clientId)
        => await _context.Inquiries.Include(i => i.Client)
            .Where(i => i.ClientId == clientId).ToListAsync();

    public async Task<Inquiry?> GetByIdAsync(int id)
        => await _context.Inquiries.Include(i => i.Client).FirstOrDefaultAsync(i => i.Id == id);

    public async Task<Inquiry> AddAsync(Inquiry inquiry)
    {
        _context.Inquiries.Add(inquiry);
        await _context.SaveChangesAsync();
        return inquiry;
    }

    public async Task UpdateAsync(Inquiry inquiry)
    {
        _context.Inquiries.Update(inquiry);
        await _context.SaveChangesAsync();
    }
}

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    public UserRepository(AppDbContext context) => _context = context;

    public async Task<AppUser?> GetByEmailAsync(string email)
        => await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

    public async Task<AppUser> AddAsync(AppUser user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }
}
