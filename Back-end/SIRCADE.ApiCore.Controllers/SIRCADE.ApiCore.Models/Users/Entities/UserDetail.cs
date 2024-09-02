using System.ComponentModel.DataAnnotations.Schema;
using SIRCADE.ApiCore.Models.Users.Enums;

namespace SIRCADE.ApiCore.Models.Users.Entities;

[Table("DetalleUsuario")]
public class UserDetail
{
    public int Id { get; set; }

    [Column("Grado")]
    public string Grade { get; set; } = default!;

    [Column("ApellidoPaterno")]
    public string PaternalLastName { get; set; } = default!;

    [Column("ApellidoMaterno")]
    public string MaternalLastName { get; set; } = default!;

    [Column("Nombres")]
    public string Names { get; set; } = default!;

    [Column("Unidad")]
    public int UnityId { get; set; } = default!;

    [Column("FechaNacimiento")]
    public DateTime BirthDate { get; set; }

    [Column("Telefono")]
    public string Phone { get; set; } = default!;

    [Column("Correo")]
    public string Email { get; set; } = default!;

    [Column("Celular")]
    public string CellPhone { get; set; } = default!;

    [Column("Afiliado")]
    public bool Associated { get; set; }

    [Column("Situacion")]
    public Situation Situation { get; set; } = default!;

    [Column("DNI")]
    public string DocumentNumber { get; set; } = default!;

    [Column("EstadoCivil")]
    public MaritalStatus MaritalStatus { get; set; }

    [Column("Direccion")]
    public string? Address { get; set; }

    [Column("Observacion")]
    public string? Observation { get; set; }

    public User User { get; set; } = default!;

    public Unity Unity { get; set; } = default!;
}