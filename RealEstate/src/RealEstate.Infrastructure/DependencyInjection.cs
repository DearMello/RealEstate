using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealEstate.Application.Interfaces;
using RealEstate.Domain.Interfaces;
using RealEstate.Infrastructure.Persistence;
using RealEstate.Infrastructure.Persistence.Repositories;
using RealEstate.Infrastructure.Services;

namespace RealEstate.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IPropertyRepository, PropertyRepository>();
        services.AddScoped<IAgentRepository, AgentRepository>();
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<IListingRepository, ListingRepository>();
        services.AddScoped<IInquiryRepository, InquiryRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}
