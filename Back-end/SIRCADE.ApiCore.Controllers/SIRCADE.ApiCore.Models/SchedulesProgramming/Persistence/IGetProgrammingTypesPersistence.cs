using SIRCADE.ApiCore.Models.SchedulesProgramming.Entities;

namespace SIRCADE.ApiCore.Models.SchedulesProgramming.Persistence;

public interface IGetProgrammingTypesPersistence
{
    Task<IEnumerable<ProgrammingType>> ExecuteAsync();
    Task<ProgrammingType> ExecuteAsync(int programmingTypeId);
}