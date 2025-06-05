using Application.Dto.Dtos.Projetos;
using Application.Interfaces;
using Domain.Filter.Filters.Projetos;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjetosController : ControllerBase
    {
        private readonly IProjetoAppService _projetoAppService;

        public ProjetosController(IProjetoAppService projetoAppService)
        {
            _projetoAppService = projetoAppService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _projetoAppService.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] ProjetosFilter filters)
        {
            var result = await _projetoAppService.GetWithFilters(filters);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProjetoDto dto)
        {
            var result = await _projetoAppService.CreateAsync(dto);
            if (result.Model == null)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProjetoDto dto)
        {
            if (id != dto.Id)
                return BadRequest(new { Message = "ID mismatch" });

            var result = await _projetoAppService.UpdateAsync(dto);
            if (result.Model == null)
                return NotFound(result);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _projetoAppService.DeleteAsync(id);
            return Ok(result);
        }
    }
}