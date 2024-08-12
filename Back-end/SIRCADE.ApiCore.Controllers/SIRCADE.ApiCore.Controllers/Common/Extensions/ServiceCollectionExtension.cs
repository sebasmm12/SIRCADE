using SIRCADE.ApiCore.Controllers.Common.Services.Contracts;
using SIRCADE.ApiCore.Controllers.Common.Services.Interfaces;
using SIRCADE.ApiCore.Controllers.Roles.Services;
using SIRCADE.ApiCore.Controllers.Roles.Services.Imp;
using SIRCADE.ApiCore.Models.Roles.Persistence;
using SIRCADE.ApiCore.Models.Roles.Persistence.Imp;

namespace SIRCADE.ApiCore.Controllers.Common.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddCommonServices(this IServiceCollection services)
    {
        services.AddScoped<IHashService, HashService>();

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<IRolesService, RolesService>();

        return services;
    }

    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddScoped<IGetRolesPersistence, GetRolesPersistence>();
        services.AddScoped<ICreateRolePersistence, CreateRolePersistence>();
        services.AddScoped<IUpdateRolePersistence, UpdateRolePersistence>();
        services.AddScoped<IDeleteRolePersistence, DeleteRolePersistence>();
        services.AddScoped<IExistRolePersistence, ExistRolePersistence>();

        return services;
    }
}