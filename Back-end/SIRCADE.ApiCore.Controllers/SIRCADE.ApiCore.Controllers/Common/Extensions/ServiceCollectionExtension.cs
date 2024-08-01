using SIRCADE.ApiCore.Controllers.Common.Services.Contracts;
using SIRCADE.ApiCore.Controllers.Common.Services.Interfaces;

namespace SIRCADE.ApiCore.Controllers.Common.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddCommonServices(this IServiceCollection services)
    {
        services.AddScoped<IHashService, HashService>();

        return services;
    }
}