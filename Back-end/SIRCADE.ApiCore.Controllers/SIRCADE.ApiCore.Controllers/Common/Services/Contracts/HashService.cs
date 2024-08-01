using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using SIRCADE.ApiCore.Controllers.Common.Services.Interfaces;
using SIRCADE.ApiCore.Models.Common.DTOs;

namespace SIRCADE.ApiCore.Controllers.Common.Services.Contracts;

public class HashService : IHashService
{
    public HashDto Generate(string password)
    {
        var salt = new byte[16];

        using var random = RandomNumberGenerator.Create();
        random.GetBytes(salt);

        return Generate(password, salt);
    }

    public bool Compare(string password, string hash, string salt)
    {
        var convertedSalt = Convert.FromBase64String(salt);

        var generatedHash = Generate(password, convertedSalt).Hash;

        return hash == generatedHash;
    }

    #region private methods
    private static HashDto Generate(string password, byte[] salt)
    {
        var pbkdf2 = KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA1, 10000, 32);

        var hash = Convert.ToBase64String(pbkdf2);

        return new(hash, Convert.ToBase64String(salt));
    }
    #endregion

}