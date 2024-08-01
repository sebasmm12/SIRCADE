namespace SIRCADE.ApiCore.Models.Common.DTOs;

public class HashDto(string hash, string salt)
{
    public string Hash { get; set; } = hash;
    public string Salt { get; set; } = salt;
}