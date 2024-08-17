namespace SIRCADE.ApiCore.Models.SportFields.Persistence.Imp;

public class UpdateSportFieldPersistence(ApplicationDbContext applicationDbContext) : IUpdateSportFieldPersistence
{
    public async Task ExecuteAsync()
    {
        await applicationDbContext.SaveChangesAsync();
    }
}