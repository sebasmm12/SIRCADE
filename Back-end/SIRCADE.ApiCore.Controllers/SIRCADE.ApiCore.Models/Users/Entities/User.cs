using System.ComponentModel.DataAnnotations.Schema;

namespace SIRCADE.ApiCore.Models.Users.Entities;

[Table("Usuarios")]
public class User
{
    public int Id { get; set; }

    public string Email { get; set; }

    [Column("Contrasena")]
    public string Password { get; set; }

    public string Salt { get; set; }
}