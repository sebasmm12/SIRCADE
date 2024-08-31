using System.ComponentModel.DataAnnotations.Schema;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Enums;
using SIRCADE.ApiCore.Models.SportFields.Entities;
using SIRCADE.ApiCore.Models.Users.Entities;

namespace SIRCADE.ApiCore.Models.SchedulesProgramming.Entities;

[Table("ProgramacionesHorarios")]
public class ScheduleProgramming
{
    public ScheduleProgramming()
    {
    }

    public ScheduleProgramming(
        int sportFieldId, 
        int? clientId, 
        DateTime startDate, 
        DateTime endDate, 
        ScheduleProgrammingState state, 
        string? comment, 
        int type, 
        int registerUserId, 
        DateTime registerDate)
    {
        SportFieldId = sportFieldId;
        ClientId = clientId;
        StartDate = startDate;
        EndDate = endDate;
        State = state;
        Comment = comment;
        Type = type;
        RegisterUserId = registerUserId;
        RegisterDate = registerDate;
    }

    public int Id { get; set; }

    [Column("IdCancha")]
    public int SportFieldId { get; set; }

    [Column("IdCliente")]
    public int? ClientId { get; set; }

    [Column("FechaInicio")]
    public DateTime StartDate { get; set; }

    [Column("FechaFin")]
    public DateTime EndDate { get; set; }

    [Column("Estado")]
    public ScheduleProgrammingState State { get; set; }

    [Column("Comentario")]
    public string? Comment { get; set; }

    [Column("Tipo")]
    public int Type { get; set; }

    [Column("IdUsuarioRegistrador")]
    public int RegisterUserId { get; set; }

    [Column("FechaRegistro")]
    public DateTime RegisterDate { get; set; }

    [Column("IdUsuarioModificador")]
    public int? ModifyUserId { get; set; }

    [Column("FechaActualizacion")]
    public DateTime? ModifyDate { get; set; }


    public SportField SportField { get; set; } = default!;

    public User? Client { get; set; }

    public ProgrammingType ProgrammingType { get; set; } = default!;
}