using System;
using System.Collections.Generic;

namespace TrazabilidadApi.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Procedure> ProcedureCreatedByUsers { get; } = new List<Procedure>();

    public virtual ICollection<Procedure> ProcedureLastModifiedUsers { get; } = new List<Procedure>();

    public virtual ICollection<UserRole> UserRoles { get; } = new List<UserRole>();
}
