using Application.Dto.Dtos.Relatorios;
using Application.Interfaces;
using Domain.Interfaces.Repository;

namespace Application.Services
{
    public class RelatorioAppService : IRelatorioAppService
    {
        private readonly ITarefaRepository _tarefaService;
        public RelatorioAppService(ITarefaRepository tarefaService)
        {
            _tarefaService = tarefaService;
        }
        public async Task<List<DesempenhoUsuarioDto>> ObterMediaTarefasConcluidasPorUsuarioUltimos30DiasAsync()
        {
            var relatorio = await _tarefaService.ObterMediaTarefasConcluidasPorUsuarioUltimos30DiasAsync();

           var result =  relatorio.GroupBy(t => t.Autor)
                .Select(g => new DesempenhoUsuarioDto
                {
                    NomeUsuario = g.First().Autor,
                    TotalConcluidas = g.Count(),
                    MediaDiaria = g.Count() / 30.0
                });
            return result.ToList();
        }
    }
}
