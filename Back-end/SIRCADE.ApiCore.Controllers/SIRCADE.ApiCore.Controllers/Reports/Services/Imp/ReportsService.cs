using SIRCADE.ApiCore.Controllers.Reports.Mappers;
using SIRCADE.ApiCore.Controllers.Reports.Responses;
using SIRCADE.ApiCore.Models.Common.DTOs;
using SIRCADE.ApiCore.Models.Common.Extensions;
using SIRCADE.ApiCore.Models.Dashboards.Enums;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Dtos;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Entities;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Enums;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Persistence;
using SIRCADE.ApiCore.Models.SportFields.DTOs;
using SIRCADE.ApiCore.Models.SportFields.Persistence;
using SIRCADE.ApiCore.Models.Users.DTOs;
using SIRCADE.ApiCore.Models.Users.Persistence;

namespace SIRCADE.ApiCore.Controllers.Reports.Services.Imp;

public class ReportsService(
    IGetUsersPersistence getUsersPersistence,
    IGetSportFieldTypesPersistence getSportFieldTypesPersistence,
    IGetSchedulesProgrammingPersistence getSchedulesProgrammingPersistence,
    IExcelFilesService excelFilesService) : IReportsService
{
    private readonly IEnumerable<OptionDto<ScheduleProgrammingState>> scheduleProgrammingStates =
    [
        new(ScheduleProgrammingState.Reserved, "Reservado"),
        new(ScheduleProgrammingState.ReScheduled, "Reprogramado"),
        new(ScheduleProgrammingState.Cancelled, "Cancelado")
    ]; 

    private readonly IEnumerable<OptionDto<TurnDatesDto>> turnDates =
    [
        new(new(360, 660), "Mañana"),
        new(new(661, 1080), "Tarde"),
        new(new(1081, 1440), "Noche")
    ];

    public async Task<DataTableDto<ReportInfoResponse>> GetFrequentlyUsersAsync(FrequentlyUserDataTableQueriesDto frequentlyUserDataTableQueriesDto, bool isPaginated = true)
    {
        var users = await getUsersPersistence.ExecuteForReportsAsync(frequentlyUserDataTableQueriesDto, isPaginated);

        var sportFieldTypes = await getSportFieldTypesPersistence.ExecuteAsync();

        var sportFieldTypeNames = sportFieldTypes.Select(sportFieldType => sportFieldType.Name);

        var frequentlyUsersByReservation =
            users.Data.MapToFrequentlyUsersByReservationResponse(sportFieldTypeNames,
                frequentlyUserDataTableQueriesDto.ReservationStates);

        var response = new DataTableDto<ReportInfoResponse>(frequentlyUsersByReservation, users.TotalElements);

        return response;
    }

    public async Task<string> ExportFrequentlyUsersAsync(FrequentlyUserExportQueriesDto frequentlyUserExportQueriesDto)
    {
        var users = await GetFrequentlyUsersAsync(frequentlyUserExportQueriesDto, false);

        var convertedExcelFile = excelFilesService.Generate(frequentlyUserExportQueriesDto.ReportTitle, "Socio", users.Data);

        return convertedExcelFile;
    }

    public async Task<string> ExportReportAsync(Func<Task<DataTableDto<ReportInfoResponse>>> reservationsAsyncFunc, string reportTitle, string reportName)
    {
        var reservations = await reservationsAsyncFunc();

        var convertedExcelFile = excelFilesService.Generate(reportTitle, reportName, reservations.Data);

        return convertedExcelFile;
    }

    public async Task<DataTableDto<ReportInfoResponse>> GetReservationsMonthlyAsync()
    {
        var reservations = await getSchedulesProgrammingPersistence.ExecuteAsync(DashboardTimeType.Monthly);

        var months = MonthsExtensions.GetMonths();

        var reservationsMonthly = scheduleProgrammingStates
                                    .Select(scheduleProgrammingState => GetReservationMonthly(reservations, scheduleProgrammingState, months))
                                    .ToList();

        var allAvailableReservations = GetAllAvailableReservations(reservationsMonthly);

        var reservedReservationsIndex = reservationsMonthly.FindIndex(reservation => reservation.Label == "Reservado");

        reservationsMonthly.RemoveAt(reservedReservationsIndex);

        reservationsMonthly.Insert(reservedReservationsIndex, allAvailableReservations);

        return new(reservationsMonthly, reservationsMonthly.Count);

    }

    public async Task<DataTableDto<ReportInfoResponse>> GetReservationsYearlyAsync()
    {
        var reservations = await getSchedulesProgrammingPersistence.ExecuteAsync(DashboardTimeType.Yearly);

        var years = YearsExtensions.GetYears(5);

        var reservationsYearly = scheduleProgrammingStates
                                    .Select(scheduleProgrammingState => GetReservationYearly(reservations, scheduleProgrammingState, years))
                                    .ToList();

        var allAvailableReservations = GetAllAvailableReservations(reservationsYearly);

        var reservedReservationsIndex = reservationsYearly.FindIndex(reservation => reservation.Label == "Reservado");

        reservationsYearly.RemoveAt(reservedReservationsIndex);

        reservationsYearly.Insert(reservedReservationsIndex, allAvailableReservations);

        return new(reservationsYearly, reservationsYearly.Count);
    }

    public async Task<DataTableDto<ReportInfoResponse>> GetReservationsDailyAsync()
    {
        var reservations = await getSchedulesProgrammingPersistence.ExecuteAsync(DashboardTimeType.Daily);

        var days = DaysExtensions.GetDays();

        var reservationsYearly = scheduleProgrammingStates
                                        .Select(scheduleProgrammingState => GetReservationDaily(reservations, scheduleProgrammingState, days))
                                        .ToList();

        var allAvailableReservations = GetAllAvailableReservations(reservationsYearly);

        var reservedReservationsIndex = reservationsYearly.FindIndex(reservation => reservation.Label == "Reservado");

        reservationsYearly.RemoveAt(reservedReservationsIndex);

        reservationsYearly.Insert(reservedReservationsIndex, allAvailableReservations);

        return new(reservationsYearly, reservationsYearly.Count);
    }

    public async Task<DataTableDto<ReportInfoResponse>> GetReservationsWeeklyAsync()
    {
        var reservations = await getSchedulesProgrammingPersistence.ExecuteAsync(DashboardTimeType.Weekly);

        var weeks = WeeksExtensions.GetWeeks();

        var reservationsYearly = scheduleProgrammingStates
                                    .Select(scheduleProgrammingState => GetReservationWeekly(reservations, scheduleProgrammingState, weeks))
                                    .ToList();

        var allAvailableReservations = GetAllAvailableReservations(reservationsYearly);

        var reservedReservationsIndex = reservationsYearly.FindIndex(reservation => reservation.Label == "Reservado");

        reservationsYearly.RemoveAt(reservedReservationsIndex);

        reservationsYearly.Insert(reservedReservationsIndex, allAvailableReservations);

        return new(reservationsYearly, reservationsYearly.Count);
    }

    public async Task<DataTableDto<ReportInfoResponse>> GetSportFieldTypesByTurnAsync(ScheduleProgrammingByTurnDto scheduleProgrammingByTurnDto)
    {
        var sportFieldTypes = await getSportFieldTypesPersistence.ExecuteAsync();

        var reservations = await getSchedulesProgrammingPersistence.ExecuteAsync(DashboardTimeType.Turn, scheduleProgrammingByTurnDto, false);


        var sportFieldTypeReservations = sportFieldTypes.Select(sportFieldType => new SportFieldReservationsDto(sportFieldType.Name,
                                                                                                                reservations.Where(reservation => reservation.SportField.Type == sportFieldType.Id)));

        var sportFieldsByTurn = turnDates
                                    .Select(turnDate => GetSportFieldsByTurn(turnDate, sportFieldTypeReservations))
                                    .ToList();

        return new(sportFieldsByTurn, sportFieldsByTurn.Count);
    }

    #region private methods

    private static ReportInfoResponse GetSportFieldsByTurn(OptionDto<TurnDatesDto> turnDatesDto,
        IEnumerable<SportFieldReservationsDto> sportFieldReservationsDtos)
    {
        var sportFieldsByTurn = sportFieldReservationsDtos.Select(sportFieldReservationsDto => new TypeQuantity(
            sportFieldReservationsDto.SportFieldType,
            sportFieldReservationsDto.Reservations.Count(sportFieldReservationDto =>
                sportFieldReservationDto.StartDate.TimeOfDay.TotalMinutes >= turnDatesDto.Id.StartMinutes &&
                sportFieldReservationDto.StartDate.TimeOfDay.TotalMinutes <= turnDatesDto.Id.EndMinutes)));

        return new(turnDatesDto.Label, sportFieldsByTurn);
    }

    private ReportInfoResponse GetReservationMonthly(IEnumerable<ScheduleProgramming> reservations, OptionDto<ScheduleProgrammingState> state, IEnumerable<OptionDto<int>> months)
    {
        var reservationsByState = reservations.Where(reservation => reservation.State == state.Id);

        var reservationsByMonth = months.Select(month => new TypeQuantity(month.Label, reservationsByState.Count(reservation => reservation.StartDate.Month == month.Id)));

        return new(state.Label, reservationsByMonth);
    }

    private static ReportInfoResponse GetReservationYearly(IEnumerable<ScheduleProgramming> reservations, OptionDto<ScheduleProgrammingState> state, IEnumerable<OptionDto<int>> years)
    {
        var reservationsByState = reservations.Where(reservation => reservation.State == state.Id);

        var reservationsByYear = years.Select(year => new TypeQuantity(year.Label, reservationsByState.Count(reservation => reservation.StartDate.Year == year.Id)));

        return new(state.Label, reservationsByYear);
    }

    private static ReportInfoResponse GetReservationDaily(IEnumerable<ScheduleProgramming> reservations, OptionDto<ScheduleProgrammingState> state, IEnumerable<OptionDto<DayOfWeek>> days)
    {
        var reservationsByState = reservations.Where(reservation => reservation.State == state.Id);

        var reservationsByDay = days.Select(day => new TypeQuantity(day.Label, reservationsByState.Count(reservation => reservation.StartDate.DayOfWeek == day.Id)));

        return new(state.Label, reservationsByDay);
    }

    private static ReportInfoResponse GetReservationWeekly(
        IEnumerable<ScheduleProgramming> reservations, OptionDto<ScheduleProgrammingState> state,
        IEnumerable<OptionDto<WeeksDto>> weeks)
    {
        var reservationsByState = reservations.Where(reservation => reservation.State == state.Id);

        var reservationsByWeek = weeks.Select(week => new TypeQuantity(week.Label, reservationsByState.Count(reservation => reservation.StartDate.DayOfYear >= week.Id.StartDay && reservation.StartDate.DayOfYear <= week.Id.EndDay)));

        return new(state.Label, reservationsByWeek);
    }

    private ReportInfoResponse GetAllAvailableReservations(IList<ReportInfoResponse> reservations)
    {
        var reservedType = scheduleProgrammingStates.First(x => x.Id == ScheduleProgrammingState.Reserved);

        var reservedReservations = reservations.First(reservation => reservation.Label == reservedType.Label);

        var reScheduledReservations = reservations.First(reservation => reservation.Label == "Reprogramado");

        var availableMonthTypeQuantities = reservedReservations
                                    .TypeQuantities
                                    .Select((monthTypeQuantity, index) => new TypeQuantity(monthTypeQuantity.Name, monthTypeQuantity.Quantity + reScheduledReservations.TypeQuantities.ElementAt(index).Quantity));

        return new(reservedType.Label, availableMonthTypeQuantities);
    }

    #endregion
}