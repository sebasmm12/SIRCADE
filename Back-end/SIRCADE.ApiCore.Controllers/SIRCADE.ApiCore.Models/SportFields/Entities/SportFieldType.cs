using System.ComponentModel.DataAnnotations.Schema;

namespace SIRCADE.ApiCore.Models.SportFields.Entities;

[Table("TiposCanchaDeportiva")]
public class SportFieldType
{
    public int Id { get; set; }

    [Column("Nombre")]
    public string Name { get; set; } = default!;

    public ICollection<SportField> SportFields { get; set; } = [];
}