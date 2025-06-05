using Application.Dto.Dtos.HistoricoAtualizacoes;
using Application.Dto.Helper;
using Domain.Entities;
using Domain.Filter.Filters.HistoricoAtualizacoes;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Services;
using FluentValidation;

namespace Domain.Services.HistoricoAtualizacoes
{
    public class HistoricoAtualizacaoServiceBase : IHistoricoAtualizacaoService
    {
        private readonly IHistoricoAtualizacaoRepository _repository;
        private readonly IValidator<HistoricoAtualizacaoDto> _validator;

        public HistoricoAtualizacaoServiceBase(IHistoricoAtualizacaoRepository repository, IValidator<HistoricoAtualizacaoDto> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public virtual async Task<HistoricoAtualizacaoResult> GetById(int id)
        {
            var entity = await _repository.GetById(id);
            if (entity == null)
            {
                var notFoundResult = ValidationResultHelper.NotFound("Id", "Não identificado");
                return new HistoricoAtualizacaoResult
                {
                    ValidationResult = notFoundResult,
                    Message = "Não encontrado"
                };
            }

            return new HistoricoAtualizacaoResult
            {
                HistoricoAtualizacao = new HistoricoAtualizacaoDto
                {
                    Id = entity.Id,
                    TarefaId = entity.TarefaId,
                    Autor = entity.Autor,
                    CampoAlterado = entity.CampoAlterado,
                    ValorAntigo = entity.ValorAntigo,
                    ValorNovo = entity.ValorNovo,
                    DataAlteracao = entity.DataAlteracao
                }
            };
        }

        public virtual async Task<(List<HistoricoAtualizacaoDto> Result, int TotalCount)> GetWithFilters(HistoricoAtualizacaoFilter filter)
        {
            (IEnumerable<HistoricoAtualizacao> Result, int TotalCount) result = await _repository.GetWithFilters(filter);
            return (result.Result.Select(s => new HistoricoAtualizacaoDto
            {
                Id = s.Id,
                TarefaId = s.TarefaId,
                Autor = s.Autor,
                CampoAlterado = s.CampoAlterado,
                ValorAntigo = s.ValorAntigo,
                ValorNovo = s.ValorNovo,
                DataAlteracao = s.DataAlteracao
            }).ToList(), result.TotalCount);
        }

        public virtual async Task<HistoricoAtualizacaoResult> Create(HistoricoAtualizacaoDto dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return new HistoricoAtualizacaoResult { ValidationResult = validationResult };

            var entity = new HistoricoAtualizacao(dto.TarefaId, dto.Autor, dto.CampoAlterado, dto.ValorAntigo, dto.ValorNovo);
            var result = _repository.Add(entity);
            await _repository.CommitAsync();

            return new HistoricoAtualizacaoResult
            {
                HistoricoAtualizacao = new HistoricoAtualizacaoDto
                {
                    Id = result.Id,
                    TarefaId = result.TarefaId,
                    Autor = result.Autor,
                    CampoAlterado = result.CampoAlterado,
                    ValorAntigo = result.ValorAntigo,
                    ValorNovo = result.ValorNovo,
                    DataAlteracao = result.DataAlteracao
                },
                ValidationResult = validationResult,
                Message = validationResult.IsValid ? "Criado com sucesso" : "Erro ao criar"
            };
        }

        public virtual async Task<HistoricoAtualizacaoResult> Update(HistoricoAtualizacaoDto dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return new HistoricoAtualizacaoResult { ValidationResult = validationResult };

            var entity = await _repository.GetById(dto.Id);
            if (entity == null)
            {
                var notFoundResult = ValidationResultHelper.NotFound(nameof(dto.Id), "Não identificado");
                return new HistoricoAtualizacaoResult
                {
                    ValidationResult = notFoundResult,
                    Message = "Não encontrado"
                };
            }

            entity.CampoAlterado = dto.CampoAlterado;
            entity.ValorAntigo = dto.ValorAntigo;
            entity.ValorNovo = dto.ValorNovo;
            entity.Autor = dto.Autor;
            entity.TarefaId = dto.TarefaId;
            entity.DataAlteracao = dto.DataAlteracao;

            var updated = _repository.Update(entity);
            await _repository.CommitAsync();

            return new HistoricoAtualizacaoResult
            {
                HistoricoAtualizacao = new HistoricoAtualizacaoDto
                {
                    Id = updated.Id,
                    TarefaId = updated.TarefaId,
                    Autor = updated.Autor,
                    CampoAlterado = updated.CampoAlterado,
                    ValorAntigo = updated.ValorAntigo,
                    ValorNovo = updated.ValorNovo,
                    DataAlteracao = updated.DataAlteracao
                },
                ValidationResult = validationResult,
                Message = validationResult.IsValid ? "Atualização realizada com sucesso" : "Erro ao atualizar"
            };
        }

        public virtual async Task<HistoricoAtualizacaoResult> Delete(int id)
        {
            var entity = await _repository.GetById(id);

            if (entity == null)
            {
                var notFoundResult = ValidationResultHelper.NotFound("Id", "Não identificado");
                return new HistoricoAtualizacaoResult { ValidationResult = notFoundResult };
            }

            _repository.Remove(entity);
            await _repository.CommitAsync();
            return new HistoricoAtualizacaoResult
            {
                Message = "Removido com sucesso"
            };
        }
    }
}
