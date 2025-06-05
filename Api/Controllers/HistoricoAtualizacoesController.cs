using Application.Dto.Dtos.HistoricoAtualizacoes;
using Application.Interfaces;
using Domain.Filter.Filters.HistoricoAtualizacoes;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistoricoAtualizacoesController : ControllerBase
    {
        private readonly IHistoricoAtualizacaoAppService _appService;

        public HistoricoAtualizacoesController(IHistoricoAtualizacaoAppService appService)
        {
            _appService = appService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _appService.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] HistoricoAtualizacaoFilter filters)
        {
            var result = await _appService.GetWithFilters(filters);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] HistoricoAtualizacaoDto dto)
        {
            var result = await _appService.CreateAsync(dto);
            if (result.Model == null)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] HistoricoAtualizacaoDto dto)
        {
            if (id != dto.Id)
                return BadRequest(new { Message = "ID mismatch" });

            var result = await _appService.UpdateAsync(dto);
            if (result.Model == null)
                return NotFound(result);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _appService.DeleteAsync(id);
            return Ok(result);
        }
    }
}
