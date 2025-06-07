using System.Threading.Tasks;
using Application.Dto.Dtos.Tarefas;
using Application.Dto.Helper;
using Domain.Constant;
using Domain.Entities;
using Domain.Filter.Filters.Tarefas;
using Domain.Interfaces.Repository;
using FluentValidation;

namespace Domain.Services.Tarefas
{
    public class TarefaService : TarefaServiceBase
    {
        private readonly IHistoricoAtualizacaoRepository _historicoAtualizacaoRepository;
        public TarefaService(ITarefaRepository tarefaRepository, IValidator<TarefaDto> validator, IHistoricoAtualizacaoRepository historicoAtualizacaoRepository)
            : base(tarefaRepository, validator)
        {
            _historicoAtualizacaoRepository = historicoAtualizacaoRepository;
        }

        public override async Task<TarefaResult> Create(TarefaDto dto)
        {
            var tarefasPendentes = await _tarefaRepository.GetWithFilters(new TarefaFilter { ProjetoId = dto.ProjetoId, IsPagination = false });

            if (tarefasPendentes.TotalCount >= TarefasConst.LimiteTarefasPorProjeto)
            {
                var validationResult = ValidationResultHelper.BusinessRule("Projeto", $"Não é possível criar mais tarefas, o limite de {TarefasConst.LimiteTarefasPorProjeto} foi atingido.");
                return new TarefaResult
                {
                    ValidationResult = validationResult,
                    Message = "Limite de tarefas atingido"
                };
            }


            return await base.Create(dto);
        }

        public override async Task<TarefaResult> Update(TarefaDto dto)
        {
            var tarefaComMesmaPrioridade = await _tarefaRepository.GetById(dto.Id);

            if (tarefaComMesmaPrioridade != null && tarefaComMesmaPrioridade.PrioridadeId != dto.PrioridadeId)
            {
                var validationResult = ValidationResultHelper.BusinessRule("Tarefa", "Não é permitido alterar a prioridade de uma tarefa depois que ela foi criada.");
                return new TarefaResult
                {
                    ValidationResult = validationResult,
                    Message = "Atualização não permitida - Prioridade já está definido"
                };
            }

            var result = await base.Update(dto);

            if (result.ValidationResult.IsValid)
            {
                var tarefaId = result.Tarefa.Id;
                await AdicionarSeAlterado(tarefaId, "Titulo", dto.Titulo, result.Tarefa.Titulo);
                await AdicionarSeAlterado(tarefaId, "Descricao", dto.Descricao, result.Tarefa.Descricao);
                await AdicionarSeAlterado(tarefaId, "Status", dto.StatusId, result.Tarefa.StatusId);
                await AdicionarSeAlterado(tarefaId, "Prioridade", dto.PrioridadeId, result.Tarefa.PrioridadeId);
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
