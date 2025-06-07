using Application.Dto.Helper;
using Domain.Entities;
using Domain.Filter.Filters.Tarefas;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Services;
using FluentValidation;
using Application.Dto.Dtos.Tarefas;
using Domain.Constant;

namespace Domain.Services.Tarefas
{
    public class TarefaServiceBase : ITarefaService
    {
        protected readonly ITarefaRepository _tarefaRepository;
        protected readonly IValidator<TarefaDto> _validator;

        public TarefaServiceBase(ITarefaRepository tarefaRepository, IValidator<TarefaDto> validator)
        {
            _tarefaRepository = tarefaRepository;
            _validator = validator;
        }

        public virtual async Task<TarefaResult> GetById(int id)
        {
            var tarefa = await _tarefaRepository.GetById(id);
            if (tarefa == null)
            {
                var notFoundResult = ValidationResultHelper.NotFound("Id", "Não identificado");
                return new TarefaResult
                {
                    ValidationResult = notFoundResult,
                    Message = "Não encontrado"
                };
            }

            return new TarefaResult
            {
                Tarefa = new TarefaDto
                {
                    Id = tarefa.Id,
                    ProjetoId = tarefa.ProjetoId,
                    Titulo = tarefa.Titulo,
                    Descricao = tarefa.Descricao,
                    DataVencimento = tarefa.DataVencimento,
                    StatusId = tarefa.StatusId,
                    PrioridadeId = tarefa.PrioridadeId,
                    DataCriacao = tarefa.DataCriacao
                }
            };
        }

        public virtual async Task<(List<TarefaDto> Result, int TotalCount)> GetWithFilters(TarefaFilter filter)
        {
            (IEnumerable<Tarefa> Result, int TotalCount) result = await _tarefaRepository.GetWithFilters(filter);
            return (result.Result.Select(t => new TarefaDto
            {
                Id = t.Id,
                ProjetoId = t.ProjetoId,
                Titulo = t.Titulo,
                Descricao = t.Descricao,
                DataVencimento = t.DataVencimento,
                StatusId = t.StatusId,
                PrioridadeId = t.PrioridadeId,
                DataCriacao = t.DataCriacao
            }).ToList(), result.TotalCount);
        }

        public virtual async Task<TarefaResult> Create(TarefaDto dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return new TarefaResult { ValidationResult = validationResult };

            var tarefa = new Tarefa(dto.ProjetoId, 
                                    dto.Titulo, 
                                    dto.Descricao, 
                                    dto.DataVencimento, 
                                    dto.StatusId, 
                                    dto.PrioridadeId, 
                                    dto.DataCriacao, 
                                    UsuarioServiceExternoConst.UserName);            
            
            
            var result = _tarefaRepository.Add(tarefa);
            await _tarefaRepository.CommitAsync();

            return new TarefaResult
            {
                Tarefa = new TarefaDto
                {
                    Id = result.Id,
                    ProjetoId = result.ProjetoId,
                    Titulo = result.Titulo,
                    Descricao = result.Descricao,
                    DataVencimento = result.DataVencimento,
                    StatusId = result.StatusId,
                    PrioridadeId = result.PrioridadeId,
                    DataCriacao = result.DataCriacao
                },
                ValidationResult = validationResult,
                Message = validationResult.IsValid ? "Criado com sucesso" : "Erro ao criar"
            };
        }

        public virtual async Task<TarefaResult> Update(TarefaDto dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return new TarefaResult { ValidationResult = validationResult };

            var tarefa = await _tarefaRepository.GetById(dto.Id);
            if (tarefa == null)
            {
                var notFoundResult = ValidationResultHelper.NotFound(nameof(dto.Id), "Não identificado");
                return new TarefaResult
                {
                    ValidationResult = notFoundResult,
                    Message = "Não encontrado"
                };
            }

            tarefa.SetTitulo(dto.Titulo);
            tarefa.SetDescricao(dto.Descricao);
            tarefa.SetDataVencimento(dto.DataVencimento);
            tarefa.SetStatusId(dto.StatusId);
            tarefa.SetPrioridadeId(dto.PrioridadeId);

            var updated = _tarefaRepository.Update(tarefa);
            await _tarefaRepository.CommitAsync();

            return new TarefaResult
            {
                Tarefa = new TarefaDto
                {
                    Id = updated.Id,
                    ProjetoId = updated.ProjetoId,
                    Titulo = updated.Titulo,
                    Descricao = updated.Descricao,
                    DataVencimento = updated.DataVencimento,
                    StatusId = updated.StatusId,
                    PrioridadeId = updated.PrioridadeId,
                    DataCriacao = updated.DataCriacao
                },
                ValidationResult = validationResult,
                Message = validationResult.IsValid ? "Atualização realizada com sucesso" : "Erro ao atualizar"
            };
        }

        public virtual async Task<TarefaResult> Delete(int id)
        {
            var tarefa = await _tarefaRepository.GetById(id);

            if (tarefa == null)
            {
                var notFoundResult = ValidationResultHelper.NotFound("Id", "Não identificado");
                return new TarefaResult { ValidationResult = notFoundResult };
            }

            _tarefaRepository.Remove(tarefa);
            await _tarefaRepository.CommitAsync();
            return new TarefaResult
            {
                Message = "Removido com sucesso"
            };
        }
    }
}
