using SIRCADE.ApiCore.Models.Notifications.Persistence;
using SIRCADE.ApiCore.Models.Notifications.Persistence.Imp;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Persistence.Imp;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Persistence;
using SIRCADE.Notifications.Worker.Services;
using SIRCADE.Notifications.Worker.Services.Imp;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Factories.Imp;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Factories;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Strategies.Imp;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Strategies;
using SIRCADE.ApiCore.Models.Permissions.Persistence.Imp;
using SIRCADE.ApiCore.Models.Permissions.Persistence;
using SIRCADE.ApiCore.Models.Roles.Persistence.Imp;
using SIRCADE.ApiCore.Models.Roles.Persistence;
using SIRCADE.ApiCore.Models.SportFields.Persistence.Imp;
using SIRCADE.ApiCore.Models.SportFields.Persistence;
using SIRCADE.ApiCore.Models.Users.Persistence.Imp;
using SIRCADE.ApiCore.Models.Users.Persistence;

namespace SIRCADE.Notifications.Worker.Common.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<INotificationsService, NotificationsService>();

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

        services.AddScoped<IGetNotificationsPersistence, GetNotificationsPersistence>();
        services.AddScoped<IGetUserNotificationsPersistence, GetUserNotificationsPersistence>();
        services.AddScoped<ICreateUserNotificationsPersistence, CreateUserNotificationsPersistence>();

        return services;
    }

    public static IServiceCollection AddStrategies(this IServiceCollection services)
    {
        services.AddScoped<ISchedulesProgrammingInTimeStrategy, SchedulesProgrammingWeeklyStrategy>();
        services.AddScoped<ISchedulesProgrammingInTimeStrategy, SchedulesProgrammingMonthlyStrategy>();
        services.AddScoped<ISchedulesProgrammingInTimeStrategy, SchedulesProgrammingYearlyStrategy>();
        services.AddScoped<ISchedulesProgrammingInTimeStrategy, SchedulesProgrammingDailyStrategy>();
        services.AddScoped<ISchedulesProgrammingInTimeStrategy, SchedulesProgrammingCurrentMonthStrategy>();
        services.AddScoped<ISchedulesProgrammingInTimeStrategy, SchedulesProgrammingByTurnStrategy>();

        return services;
    }

    public static IServiceCollection AddFactories(this IServiceCollection services)
    {
        services.AddScoped<ISchedulesProgrammingInTimeFactory, SchedulesProgrammingInTimeFactory>();

        return services;
    }
}