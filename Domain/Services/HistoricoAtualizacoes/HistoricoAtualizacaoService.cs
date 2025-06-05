using Application.Dto.Dtos.HistoricoAtualizacoes;
using Domain.Interfaces.Repository;
using FluentValidation;

namespace Domain.Services.HistoricoAtualizacoes
{
    public class HistoricoAtualizacaoService : HistoricoAtualizacaoServiceBase
    {
        public HistoricoAtualizacaoService(IHistoricoAtualizacaoRepository historicoAtualizacaoRepository, IValidator<HistoricoAtualizacaoDto> validator)
            : base(historicoAtualizacaoRepository, validator)
        {
        }
    }
}
