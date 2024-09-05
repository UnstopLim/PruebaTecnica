using System;
using System.Collections.Generic;

namespace TrazabilidadApi.Models;

public partial class Field
{
    public int FieldId { get; set; }
    public string FieldName { get; set; } = null!;

    public string DataType { get; set; } = null!;

    public virtual ICollection<DataSet> DataSets { get; } = new List<DataSet>();
}
