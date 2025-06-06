using Application.Dto.Dtos.Tarefas;
using Application.Dto.Helper;
using Domain.Constant;
using Domain.Filter.Filters.Tarefas;
using Domain.Interfaces.Repository;
using FluentValidation;

namespace Domain.Services.Tarefas
{
    public class TarefaService : TarefaServiceBase
    {
        public TarefaService(ITarefaRepository tarefaRepository, IValidator<TarefaDto> validator)
            : base(tarefaRepository, validator)
        {
        }

        public override async Task<TarefaResult> Create(TarefaDto dto)
        {
            var tarefasPendentes = await _tarefaRepository.GetWithFilters(new TarefaFilter { ProjetoId = dto.ProjetoId, IsPagination = false });

            if(tarefasPendentes.TotalCount >= TarefasConst.LimiteTarefasPorProjeto)
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

            if(tarefaComMesmaPrioridade != null && tarefaComMesmaPrioridade.PrioridadeId != dto.PrioridadeId)
            {
                var validationResult = ValidationResultHelper.BusinessRule("Tarefa", "Não é permitido alterar a prioridade de uma tarefa depois que ela foi criada.");
                return new TarefaResult
                {
                    ValidationResult = validationResult,
                    Message = "Atualização não permitida - Prioridade já está definido"
                };
            }

            return await base.Update(dto);
        }

    }
}
