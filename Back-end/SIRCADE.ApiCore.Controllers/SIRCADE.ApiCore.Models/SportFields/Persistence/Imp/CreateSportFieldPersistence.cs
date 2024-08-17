using SIRCADE.ApiCore.Models.SportFields.Entities;

namespace SIRCADE.ApiCore.Models.SportFields.Persistence.Imp;

public class CreateSportFieldPersistence(ApplicationDbContext applicationDbContext) : ICreateSportFieldPersistence
{
    public async Task<int> ExecuteAsync(SportField sportField)
    {
        await applicationDbContext.AddAsync(sportField);

        await applicationDbContext.SaveChangesAsync();

        return sportField.Id;
    }
}