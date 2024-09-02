using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SIRCADE.ApiCore.Controllers.Common.Services.Contracts;
using SIRCADE.ApiCore.Controllers.Common.Services.Interfaces;
using SIRCADE.ApiCore.Controllers.Permissions.Services;
using SIRCADE.ApiCore.Controllers.Permissions.Services.Imp;
using SIRCADE.ApiCore.Controllers.Roles.Services;
using SIRCADE.ApiCore.Controllers.Roles.Services.Imp;
using SIRCADE.ApiCore.Controllers.SchedulesProgramming.Services;
using SIRCADE.ApiCore.Controllers.SchedulesProgramming.Services.Imp;
using SIRCADE.ApiCore.Controllers.SportFields.Services;
using SIRCADE.ApiCore.Controllers.SportFields.Services.Imp;
using SIRCADE.ApiCore.Controllers.Users.Services;
using SIRCADE.ApiCore.Controllers.Users.Services.Imp;
using SIRCADE.ApiCore.Models.Permissions.Persistence;
using SIRCADE.ApiCore.Models.Permissions.Persistence.Imp;
using SIRCADE.ApiCore.Models.Roles.Persistence;
using SIRCADE.ApiCore.Models.Roles.Persistence.Imp;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Persistence;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Persistence.Imp;
using SIRCADE.ApiCore.Models.SportFields.Persistence;
using SIRCADE.ApiCore.Models.SportFields.Persistence.Imp;
using SIRCADE.ApiCore.Models.Users.Persistence;
using SIRCADE.ApiCore.Models.Users.Persistence.Imp;
using System.Text;
using SIRCADE.ApiCore.Controllers.Accounts.Services;
using SIRCADE.ApiCore.Controllers.Accounts.Services.Imp;

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
        services.AddTransient<IPermissionsService, PermissionsService>();
        services.AddTransient<IProgrammingTypesService, ProgrammingTypesService>();
        services.AddTransient<ISchedulesProgrammingService, SchedulesProgrammingService>();
        services.AddTransient<IAccountsService, AccountsService>();
        services.AddTransient<IUnitiesService, UnitiesService>();

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
        services.AddScoped<IGetUsersPersistence, GetUsersPersistence>();
        services.AddScoped<IUpdateUsersPersistence, UpdateUserPersistence>();

        services.AddScoped<IGetPermissionsPersistence, GetPermissionsPersistence>();

        services.AddScoped<IGetProgrammingTypesPersistence, GetProgrammingTypesPersistence>();

        services.AddScoped<ICreateScheduleProgrammingPersistence, CreateScheduleProgrammingPersistence>();
        services.AddScoped<IGetSchedulesProgrammingPersistence, GetSchedulesProgrammingPersistence>();
        services.AddScoped<IUpdateScheduleProgrammingPersistence, UpdateScheduleProgrammingPersistence>();
        services.AddScoped<ICountSchedulesProgrammingPersistence, CountSchedulesProgrammingPersistence>();

        services.AddScoped<IGetUnitiesPersistence, GetUnitiesPersistence>();

        return services;
    }

    public static IServiceCollection AddAuthentication(this IServiceCollection services, WebApplicationBuilder applicationBuilder)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new()
            {
                ValidateIssuer = true,
                ValidIssuer = applicationBuilder.Configuration.GetValue<string>("JWT:Issuer"),
                ValidateAudience = true,
                ValidAudience = applicationBuilder.Configuration.GetValue<string>("JWT:Audience"),
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(applicationBuilder.Configuration.GetValue<string>("JWT:Key")!)),
                ClockSkew = TimeSpan.Zero
            });

        services.AddSingleton<IBearerTokenService, BearerTokenService>();

        return services;
    }
}