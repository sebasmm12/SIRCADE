﻿using System.ComponentModel.DataAnnotations.Schema;

namespace SIRCADE.ApiCore.Models.SportFields.Entities;

[Table("CanchasDeportivas")]
public class SportField
{
    public SportField()
    {
        
    }

    public SportField(string name, int type)
    {
        Name = name;
        Type = type;
    }

    public int Id { get; set; }

    [Column("Tipo")]
    public int Type { get; set; }

    [Column("Nombre")]
    public string Name { get; set; } = default!;

    public SportFieldType SportFieldType { get; set; } = default!;
}