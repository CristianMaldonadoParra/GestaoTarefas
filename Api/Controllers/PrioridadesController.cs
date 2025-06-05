using Application.Dto.Dtos.Prioridades;
using Application.Interfaces;
using Domain.Filter.Filters.Prioridades;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrioridadesController : ControllerBase
    {
        private readonly IPrioridadeAppService _prioridadeAppService;

        public PrioridadesController(IPrioridadeAppService prioridadeAppService)
        {
            _prioridadeAppService = prioridadeAppService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _prioridadeAppService.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PrioridadeFilter filters)
        {
            var result = await _prioridadeAppService.GetWithFilters(filters);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PrioridadeDto dto)
        {
            var result = await _prioridadeAppService.CreateAsync(dto);
            if (result.Model == null)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PrioridadeDto dto)
        {
            if (id != dto.Id)
                return BadRequest(new { Message = "ID mismatch" });

            var result = await _prioridadeAppService.UpdateAsync(dto);
            if (result.Model == null)
                return NotFound(result);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _prioridadeAppService.DeleteAsync(id);
            return Ok(result);
        }
    }
}
