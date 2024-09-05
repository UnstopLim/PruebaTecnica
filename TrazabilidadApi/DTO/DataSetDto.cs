namespace TrazabilidadApi.DTO
{
    public class DataSetDto
    {
        public int DataSetID { get; set; }
        public string DataSetName { get; set; }
        public string Description { get; set; }
        public ProcedureDto Procedure { get; set; }
        public FieldDto Field { get; set; }
    }
}
