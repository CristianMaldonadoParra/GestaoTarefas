using Application.Dto.Dtos.StatusTarefas;
using Application.Interfaces;
using Domain.Filter.Filters.StatusTarefa;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatusTarefasController : ControllerBase
    {
        private readonly IStatusTarefaAppService _statusTarefaAppService;

        public StatusTarefasController(IStatusTarefaAppService statusTarefaAppService)
        {
            _statusTarefaAppService = statusTarefaAppService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _statusTarefaAppService.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] StatusTarefaFilter filters)
        {
            var result = await _statusTarefaAppService.GetWithFilters(filters);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StatusTarefaDto dto)
        {
            var result = await _statusTarefaAppService.CreateAsync(dto);
            if (result.Model == null)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] StatusTarefaDto dto)
        {
            if (id != dto.Id)
                return BadRequest(new { Message = "ID mismatch" });

            var result = await _statusTarefaAppService.UpdateAsync(dto);
            if (result.Model == null)
                return NotFound(result);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _statusTarefaAppService.DeleteAsync(id);
            return Ok(result);
        }
    }
}
