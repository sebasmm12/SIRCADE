using SIRCADE.ApiCore.Controllers.Common.Services.Contracts;
using SIRCADE.ApiCore.Controllers.Common.Services.Interfaces;
using SIRCADE.ApiCore.Controllers.Roles.Services;
using SIRCADE.ApiCore.Controllers.Roles.Services.Imp;
using SIRCADE.ApiCore.Controllers.SportFields.Services;
using SIRCADE.ApiCore.Controllers.SportFields.Services.Imp;
using SIRCADE.ApiCore.Controllers.Users.Services;
using SIRCADE.ApiCore.Controllers.Users.Services.Imp;
using SIRCADE.ApiCore.Models.Roles.Persistence;
using SIRCADE.ApiCore.Models.Roles.Persistence.Imp;
using SIRCADE.ApiCore.Models.SportFields.Persistence;
using SIRCADE.ApiCore.Models.SportFields.Persistence.Imp;
using SIRCADE.ApiCore.Models.Users.Persistence;
using SIRCADE.ApiCore.Models.Users.Persistence.Imp;

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
        services.AddTransient<ISportFieldTypesService, SportFieldTypesService>();
        services.AddTransient<ISportFieldsService, SportFieldsService>();
        services.AddTransient<IUsersService, UsersService>();

        return services;
    }

    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddScoped<IGetRolesPersistence, GetRolesPersistence>();
        services.AddScoped<ICreateRolePersistence, CreateRolePersistence>();
        services.AddScoped<IUpdateRolePersistence, UpdateRolePersistence>();
        services.AddScoped<IDeleteRolePersistence, DeleteRolePersistence>();
        services.AddScoped<IExistRolePersistence, ExistRolePersistence>();

        services.AddScoped<IGetSportFieldTypesPersistence, GetSportFieldTypesPersistence>();
        services.AddScoped<IGetSportFieldsPersistence, GetSportFieldsPersistence>();
        services.AddScoped<ICreateSportFieldPersistence, CreateSportFieldPersistence>();
        services.AddScoped<IUpdateSportFieldPersistence, UpdateSportFieldPersistence>();

        services.AddScoped<ICreateUserPersistence, CreateUserPersistence>();


        return services;
    }
}