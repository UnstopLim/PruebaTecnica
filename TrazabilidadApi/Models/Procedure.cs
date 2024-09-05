using System;
using System.Collections.Generic;

namespace TrazabilidadApi.Models;

public partial class Procedure
{
    public int ProcedureId { get; set; }

    public string? ProcedureName { get; set; }

    public string? Description { get; set; }

    public int? CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? LastModifiedUserId { get; set; }

    public DateTime? LastModifiedDate { get; set; }

    public virtual User? CreatedByUser { get; set; }

    public virtual ICollection<DataSet> DataSets { get; } = new List<DataSet>();

    public virtual User? LastModifiedUser { get; set; }
}
