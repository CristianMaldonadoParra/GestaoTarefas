using Application.Dto.Dtos.Relatorios;

namespace Application.Interfaces
{
    public interface IRelatorioAppService
    {
        Task<List<DesempenhoUsuarioDto>> ObterMediaTarefasConcluidasPorUsuarioUltimos30DiasAsync();
    }
}
