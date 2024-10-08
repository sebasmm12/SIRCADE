﻿using System.ComponentModel.DataAnnotations.Schema;

namespace SIRCADE.ApiCore.Models.Users.Entities;

[Table("Unidades")]
public class Unity
{
    public int Id { get; set; }

    [Column("Nombre")]
    public string Name { get; set; } = default!;

    [Column("Ubicacion")]
    public string? Location { get; set; } = default!;

    [Column("Siglas")]
    public string Acronym { get; set; } = default!;

    public ICollection<UserDetail> UserDetails { get; set; } = default!;
}