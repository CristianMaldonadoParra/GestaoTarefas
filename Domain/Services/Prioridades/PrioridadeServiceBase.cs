using Application.Dto.Helper;
using Domain.Entities;
using Domain.Filter.Filters.Prioridades;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Services;
using FluentValidation;
using Application.Dto.Dtos.Prioridades;

namespace Domain.Services.Prioridades
{
    public class PrioridadeServiceBase : IPrioridadeService
    {
        private readonly IPrioridadeRepository _prioridadeRepository;
        private readonly IValidator<PrioridadeDto> _validator;

        public PrioridadeServiceBase(IPrioridadeRepository prioridadeRepository, IValidator<PrioridadeDto> validator)
        {
            _prioridadeRepository = prioridadeRepository;
            _validator = validator;
        }

        public virtual async Task<PrioridadeResult> GetById(int id)
        {
            var prioridade = await _prioridadeRepository.GetById(id);
            if (prioridade == null)
            {
                var notFoundResult = ValidationResultHelper.NotFound("Id", "Não identificado");
                return new PrioridadeResult
                {
                    ValidationResult = notFoundResult,
                    Message = "Não encontrado"
                };
            }

            return new PrioridadeResult
            {
                Prioridade = new PrioridadeDto
                {
                    Id = prioridade.Id,
                    Nome = prioridade.Nome
                }
            };
        }

        public virtual async Task<(List<PrioridadeDto> Result, int TotalCount)> GetWithFilters(PrioridadeFilter filter)
        {
            (IEnumerable<Prioridade> Result, int TotalCount) result = await _prioridadeRepository.GetWithFilters(filter);
            return (result.Result.Select(s => new PrioridadeDto
            {
                Id = s.Id,
                Nome = s.Nome
            }).ToList(), result.TotalCount);
        }

        public virtual async Task<PrioridadeResult> Create(PrioridadeDto dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return new PrioridadeResult { ValidationResult = validationResult };

            var prioridade = new Prioridade(dto.Nome);
            var result = _prioridadeRepository.Add(prioridade);
            await _prioridadeRepository.CommitAsync();

            return new PrioridadeResult
            {
                Prioridade = new PrioridadeDto
                {
                    Id = result.Id,
                    Nome = result.Nome
                },
                ValidationResult = validationResult,
                Message = validationResult.IsValid ? "Criado com sucesso" : "Erro ao criar"
            };
        }

        public virtual async Task<PrioridadeResult> Update(PrioridadeDto dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return new PrioridadeResult { ValidationResult = validationResult };

            var prioridade = await _prioridadeRepository.GetById(dto.Id);
            if (prioridade == null)
            {
                var notFoundResult = ValidationResultHelper.NotFound(nameof(dto.Id), "Não identificado");
                return new PrioridadeResult
                {
                    ValidationResult = notFoundResult,
                    Message = "Não encontrado"
                };
            }

            prioridade.SetNome(dto.Nome);

            var updated = _prioridadeRepository.Update(prioridade);
            await _prioridadeRepository.CommitAsync();

            return new PrioridadeResult
            {
                Prioridade = new PrioridadeDto
                {
                    Id = updated.Id,
                    Nome = updated.Nome
                },
                ValidationResult = validationResult,
                Message = validationResult.IsValid ? "Atualização realizada com sucesso" : "Erro ao atualizar"
            };
        }

        public virtual async Task<PrioridadeResult> Delete(int id)
        {
            var prioridade = await _prioridadeRepository.GetById(id);

            if (prioridade == null)
            {
                var notFoundResult = ValidationResultHelper.NotFound("Id", "Não identificado");
                return new PrioridadeResult { ValidationResult = notFoundResult };
            }

            _prioridadeRepository.Remove(prioridade);
            await _prioridadeRepository.CommitAsync();
            return new PrioridadeResult
            {
                Message = "Removido com sucesso"
            };
        }
    }
}
