using Microsoft.Extensions.DependencyInjection;
using RealEstate.Application.Interfaces;
using RealEstate.Application.Services;

namespace RealEstate.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IPropertyService, PropertyService>();
        services.AddScoped<IAgentService, AgentService>();
        services.AddScoped<IClientService, ClientService>();
        services.AddScoped<IListingService, ListingService>();
        return services;
    }
}
