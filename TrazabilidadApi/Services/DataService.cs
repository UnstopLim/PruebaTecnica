using Microsoft.EntityFrameworkCore;
using TrazabilidadApi.DTO;
using TrazabilidadApi.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace TrazabilidadApi.Services
{
    public class DataService
    {
        private readonly NoxunContext _context;

        public DataService(NoxunContext context)
        {
            _context = context;
        }

        public async Task<List<DataSetDto>> GetDataSetsByUserIdAsync(int userId)
        {
            return await _context.DataSets
                .Include(ds => ds.Procedure)
                .Include(ds => ds.Field)
                .Where(ds => ds.Procedure.CreatedByUserId == userId || ds.Procedure.LastModifiedUserId == userId)
                .Select(ds => new DataSetDto
                {
                    DataSetID = ds.DataSetId,
                    DataSetName = ds.DataSetName,
                    Description = ds.Description,
                    Procedure = new ProcedureDto
                    {
                        ProcedureID = ds.Procedure.ProcedureId,
                        ProcedureName = ds.Procedure.ProcedureName,
                        Description = ds.Procedure.Description,
                        CreatedByUserID = ds.Procedure.CreatedByUserId,
                        CreatedDate = ds.Procedure.CreatedDate,
                        LastModifiedUserID = ds.Procedure.LastModifiedUserId,
                        LastModifiedDate = ds.Procedure.LastModifiedDate
                    }
                }).ToListAsync();
        }




        public async Task<DataSetDto> CreateDataSetAsync(CreateDataSetDto dto)
        {
            try
            {
                // Validar la existencia del Procedure
                var procedure = await _context.Procedures
                    .FirstOrDefaultAsync(p => p.ProcedureId == dto.ProcedureID);

                if (procedure == null)
                {
                    throw new ArgumentException($"Procedure with ID {dto.ProcedureID} does not exist.");
                }

                // Validar la existencia del Field
                var field = await _context.Fields
                    .FirstOrDefaultAsync(f => f.FieldId == dto.FieldID);

                if (field == null)
                {
                    throw new ArgumentException($"Field with ID {dto.FieldID} does not exist.");
                }

                // Crear el nuevo DataSet
                var newDataSet = new DataSet
                {
                    DataSetName = dto.DataSetName,
                    Description = dto.Description,
                    ProcedureId = dto.ProcedureID,
                    FieldId = dto.FieldID,
                    CreatedDate = DateTime.UtcNow
                };

                // Agregar el nuevo DataSet al contexto y guardar los cambios
                _context.DataSets.Add(newDataSet);
                await _context.SaveChangesAsync();

                // Obtener el DataSet creado para devolverlo con los detalles
                var createdDataSet = await _context.DataSets
                    .Include(ds => ds.Procedure)
                    .Include(ds => ds.Field)
                    .FirstOrDefaultAsync(ds => ds.DataSetId == newDataSet.DataSetId);

                // Mapear el DataSet a DataSetDto
                return new DataSetDto
                {
                    DataSetID = createdDataSet.DataSetId,
                    DataSetName = createdDataSet.DataSetName,
                    Description = createdDataSet.Description,
                    Procedure = new ProcedureDto
                    {
                        ProcedureID = createdDataSet.Procedure.ProcedureId,
                        ProcedureName = createdDataSet.Procedure.ProcedureName,
                        Description = createdDataSet.Procedure.Description,
                        CreatedByUserID = createdDataSet.Procedure.CreatedByUserId,
                        CreatedDate = createdDataSet.Procedure.CreatedDate,
                        LastModifiedUserID = createdDataSet.Procedure.LastModifiedUserId,
                        LastModifiedDate = createdDataSet.Procedure.LastModifiedDate
                    },
                    Field = new FieldDto
                    {
                        FieldID = createdDataSet.Field.FieldId,
                        FieldName = createdDataSet.Field.FieldName,
                        DataType = createdDataSet.Field.DataType
                    }
                };
            }
            catch (Exception ex)
            {
                // Registrar el error para diagnóstico
                // Ejemplo: _logger.LogError(ex, "Error creating dataset");
                throw; // O maneja el error de otra manera
            }
        }





    }
}
