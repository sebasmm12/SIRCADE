﻿using System.ComponentModel.DataAnnotations.Schema;
using SIRCADE.ApiCore.Models.Roles.Entities;

namespace SIRCADE.ApiCore.Models.Users.Entities;

[Table("Usuarios")]
public class User
{
    public int Id { get; set; }

    public string NSA { get; set; } = default!;

    [Column("Contrasena")]
    public string Password { get; set; } = default!;

    public string Salt { get; set; } = default!;

    [Column("IdRol")]
    public int RoleId { get; set; }

    public Role Role { get; set; } = default!;

    public UserDetail Detail { get; set; } = default!;
}