using Microsoft.AspNetCore.Mvc;
using TrazabilidadApi.DTO;
using TrazabilidadApi.Models;
using TrazabilidadApi.Services;

namespace TrazabilidadApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class DataSetsController: ControllerBase
    {
        private readonly DataService _dataService;

        public DataSetsController(DataService dataService)
        {
            _dataService = dataService;
        }


        [HttpPost]
        public async Task<IActionResult> CreateDataSet([FromBody] CreateDataSetDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid data.");
            }

            try
            {
                var dataSet = await _dataService.CreateDataSetAsync(dto);
                // Redirigir a la acción que devuelve un solo recurso por ID
                return CreatedAtAction(nameof(GetDataSetById), new { id = dataSet.DataSetID }, dataSet);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Registrar el error interno para diagnóstico
                var innerExceptionMessage = ex.InnerException?.Message ?? "No additional details available.";
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating dataset: {ex.Message}. Details: {innerExceptionMessage}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDataSetById(int id)
        {
            var dataSets = await _dataService.GetDataSetsByUserIdAsync(id);
            if (dataSets == null || !dataSets.Any())
            {
                return NotFound(new { Message = "No datasets found for the specified user." });
            }
            return Ok(dataSets);
        }




    }
}
