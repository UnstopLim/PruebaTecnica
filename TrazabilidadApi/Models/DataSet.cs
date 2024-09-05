using System;
using System.Collections.Generic;

namespace TrazabilidadApi.Models;

public partial class DataSet
{
    public int DataSetId { get; set; }

    public string? DataSetName { get; set; }

    public string? Description { get; set; }

    public int? ProcedureId { get; set; }

    public int? FieldId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? LastModifiedDate { get; set; }

    public virtual Field? Field { get; set; }

    public virtual Procedure? Procedure { get; set; }

}
