using Application.Dto.Helper;
using Domain.Entities;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Services;
using FluentValidation;
using Application.Dto.Dtos.StatusTarefas;
using Domain.Filter.Filters.StatusTarefa;

namespace Domain.Services.StatusTarefas
{
    public class StatusTarefaServiceBase : IStatusTarefaService
    {
        private readonly IStatusTarefaRepository _statusTarefaRepository;
        private readonly IValidator<StatusTarefaDto> _validator;

        public StatusTarefaServiceBase(IStatusTarefaRepository statusTarefaRepository, IValidator<StatusTarefaDto> validator)
        {
            _statusTarefaRepository = statusTarefaRepository;
            _validator = validator;
        }

        public virtual async Task<StatusTarefaResult> GetById(int id)
        {
            var statusTarefa = await _statusTarefaRepository.GetById(id);
            if (statusTarefa == null)
            {
                var notFoundResult = ValidationResultHelper.NotFound("Id", "Não identificado");
                return new StatusTarefaResult
                {
                    ValidationResult = notFoundResult,
                    Message = "Não encontrado"
                };
            }

            return new StatusTarefaResult
            {
                StatusTarefa = new StatusTarefaDto
                {
                    Id = statusTarefa.Id,
                    Nome = statusTarefa.Nome
                }
            };
        }

        public virtual async Task<(List<StatusTarefaDto> Result, int TotalCount)> GetWithFilters(StatusTarefaFilter filter)
        {
            (IEnumerable<StatusTarefa> Result, int TotalCount) result = await _statusTarefaRepository.GetWithFilters(filter);
            return (result.Result.Select(s => new StatusTarefaDto
            {
                Id = s.Id,
                Nome = s.Nome
            }).ToList(), result.TotalCount);
        }

        public virtual async Task<StatusTarefaResult> Create(StatusTarefaDto dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return new StatusTarefaResult { ValidationResult = validationResult };

            var statusTarefa = new StatusTarefa(dto.Nome);
            var result = _statusTarefaRepository.Add(statusTarefa);
            await _statusTarefaRepository.CommitAsync();

            return new StatusTarefaResult
            {
                StatusTarefa = new StatusTarefaDto
                {
                    Id = result.Id,
                    Nome = result.Nome
                },
                ValidationResult = validationResult,
                Message = validationResult.IsValid ? "Criado com sucesso" : "Erro ao criar"
            };
        }

        public virtual async Task<StatusTarefaResult> Update(StatusTarefaDto dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return new StatusTarefaResult { ValidationResult = validationResult };

            var statusTarefa = await _statusTarefaRepository.GetById(dto.Id);
            if (statusTarefa == null)
            {
                var notFoundResult = ValidationResultHelper.NotFound(nameof(dto.Id), "Não identificado");
                return new StatusTarefaResult
                {
                    ValidationResult = notFoundResult,
                    Message = "Não encontrado"
                };
            }

            statusTarefa.SetNome(dto.Nome);

            var updated = _statusTarefaRepository.Update(statusTarefa);
            await _statusTarefaRepository.CommitAsync();

            return new StatusTarefaResult
            {
                StatusTarefa = new StatusTarefaDto
                {
                    Id = updated.Id,
                    Nome = updated.Nome
                },
                ValidationResult = validationResult,
                Message = validationResult.IsValid ? "Atualização realizada com sucesso" : "Erro ao atualizar"
            };
        }

        public virtual async Task<StatusTarefaResult> Delete(int id)
        {
            var statusTarefa = await _statusTarefaRepository.GetById(id);

            if (statusTarefa == null)
            {
                var notFoundResult = ValidationResultHelper.NotFound("Id", "Não identificado");
                return new StatusTarefaResult { ValidationResult = notFoundResult };
            }

            _statusTarefaRepository.Remove(statusTarefa);
            await _statusTarefaRepository.CommitAsync();
            return new StatusTarefaResult
            {
                Message = "Removido com sucesso"
            };
        }
    }
}
