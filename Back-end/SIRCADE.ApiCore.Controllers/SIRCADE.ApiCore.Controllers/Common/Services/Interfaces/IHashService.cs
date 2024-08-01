using SIRCADE.ApiCore.Models.Common.DTOs;

namespace SIRCADE.ApiCore.Controllers.Common.Services.Interfaces;

public interface IHashService
{
    public HashDto Generate(string password);

    bool Compare(string password, string hash, string salt);
}