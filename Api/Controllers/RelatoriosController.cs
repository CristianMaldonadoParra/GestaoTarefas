using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelatoriosController : ControllerBase
    {
        private readonly IRelatorioAppService _relatorioService;

        public RelatoriosController(IRelatorioAppService relatorioService)
        {
            _relatorioService = relatorioService;
        }

        [HttpGet("DesempenhoUsuarios")]
        // APENAS GERENTES PODEM ACESSAR
        //[Authorize(Roles = "gerente")] 
        public async Task<IActionResult> GetDesempenhoUsuarios()
        {
            var resultado = await _relatorioService.ObterMediaTarefasConcluidasPorUsuarioUltimos30DiasAsync();
            return Ok(resultado);
        }
    }
}
