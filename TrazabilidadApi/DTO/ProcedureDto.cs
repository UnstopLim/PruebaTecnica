namespace TrazabilidadApi.DTO
{
    public class ProcedureDto
    {
        public int ProcedureID { get; set; }
        public string ProcedureName { get; set; }
        public string Description { get; set; }
        public int? CreatedByUserID { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? LastModifiedUserID { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
