using Application.Dto.Dtos.Comentarios;
using Domain.Constant;
using Domain.Entities;
using Domain.Interfaces.Repository;
using FluentValidation;

namespace Domain.Services.Comentarios
{
    public class ComentarioService : ComentarioServiceBase
    {
        private readonly IHistoricoAtualizacaoRepository _historicoAtualizacaoRepository;
        public ComentarioService(IComentarioRepository comentarioRepository, IValidator<ComentarioDto> validator, IHistoricoAtualizacaoRepository historicoAtualizacaoRepository)
            : base(comentarioRepository, validator)
        {
            _historicoAtualizacaoRepository = historicoAtualizacaoRepository;
        }

        public override async Task<ComentarioResult> Create(ComentarioDto dto)
        {

            var result = await base.Create(dto);

            if (result.ValidationResult.IsValid)
            {
                var tarefaId = result.Comentario.TarefaId;
                await AdicionarSeAlterado(tarefaId, "Texto", dto.Texto, result.Comentario.Texto);
            }
            return result;
        }

        private async Task AdicionarSeAlterado(int tarefaId, string campo, object novoValor, object valorAntigo)
        {
            if (!Equals(novoValor, valorAntigo))
            {
                _historicoAtualizacaoRepository.Add(
                    new HistoricoAtualizacao(
                        tarefaId,
                        UsuarioServiceExternoConst.UserName,
                        campo,
                        novoValor?.ToString(),
                        valorAntigo?.ToString()
                    )
                );
                await _historicoAtualizacaoRepository.CommitAsync();
            }
        }
    }
}
