using Application.Dto.Dtos.Tarefas;
using Application.Interfaces;
using Domain.Filter.Filters.Tarefas;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TarefasController : ControllerBase
    {
        private readonly ITarefaAppService _tarefaAppService;

        public TarefasController(ITarefaAppService tarefaAppService)
        {
            _tarefaAppService = tarefaAppService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _tarefaAppService.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] TarefaFilter filters)
        {
            var result = await _tarefaAppService.GetWithFilters(filters);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TarefaDto dto)
        {
            var result = await _tarefaAppService.CreateAsync(dto);
            if (result.Model == null)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TarefaDto dto)
        {
            if (id != dto.Id)
                return BadRequest(new { Message = "ID mismatch" });

            var result = await _tarefaAppService.UpdateAsync(dto);
            if (result.Model == null)
                return NotFound(result);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _tarefaAppService.DeleteAsync(id);
            return Ok(result);
        }
    }
}
