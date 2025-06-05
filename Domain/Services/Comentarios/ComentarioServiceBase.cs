using Application.Dto.Dtos.Comentarios;
using Application.Dto.Helper;
using Domain.Entities;
using Domain.Filter.Filters.Comentarios;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Services;
using FluentValidation;

namespace Domain.Services.Comentarios
{
    public class ComentarioServiceBase : IComentarioService
    {
        private readonly IComentarioRepository _comentarioRepository;
        private readonly IValidator<ComentarioDto> _validator;

        public ComentarioServiceBase(IComentarioRepository comentarioRepository, IValidator<ComentarioDto> validator)
        {
            _comentarioRepository = comentarioRepository;
            _validator = validator;
        }

        public virtual async Task<ComentarioResult> GetById(int id)
        {
            var comentario = await _comentarioRepository.GetById(id);
            if (comentario == null)
            {
                var notFoundResult = ValidationResultHelper.NotFound("Id", "Não identificado");
                return new ComentarioResult
                {
                    ValidationResult = notFoundResult,
                    Message = "Não encontrado"
                };
            }

            return new ComentarioResult
            {
                Comentario = new ComentarioDto
                {
                    Id = comentario.Id,
                    TarefaId = comentario.TarefaId,
                    Autor = comentario.Autor,
                    Texto = comentario.Texto,
                    DataCriacao = comentario.DataCriacao
                }
            };
        }

        public virtual async Task<(List<ComentarioDto> Result, int TotalCount)> GetWithFilters(ComentarioFilter filter)
        {
            (IEnumerable<Comentario> Result, int TotalCount) result = await _comentarioRepository.GetWithFilters(filter);
            return (result.Result.Select(s => new ComentarioDto
            {
                Id = s.Id,
                TarefaId = s.TarefaId,
                Autor = s.Autor,
                Texto = s.Texto,
                DataCriacao = s.DataCriacao
            }).ToList(), result.TotalCount);
        }

        public virtual async Task<ComentarioResult> Create(ComentarioDto dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return new ComentarioResult { ValidationResult = validationResult };

            var comentario = new Comentario(dto.TarefaId, dto.Autor, dto.Texto);
            var result = _comentarioRepository.Add(comentario);
            await _comentarioRepository.CommitAsync();

            return new ComentarioResult
            {
                Comentario = new ComentarioDto
                {
                    Id = result.Id,
                    TarefaId = result.TarefaId,
                    Autor = result.Autor,
                    Texto = result.Texto,
                    DataCriacao = result.DataCriacao
                },
                ValidationResult = validationResult,
                Message = validationResult.IsValid ? "Criado com sucesso" : "Erro ao criar"
            };
        }

        public virtual async Task<ComentarioResult> Update(ComentarioDto dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return new ComentarioResult { ValidationResult = validationResult };

            var comentario = await _comentarioRepository.GetById(dto.Id);
            if (comentario == null)
            {
                var notFoundResult = ValidationResultHelper.NotFound(nameof(dto.Id), "Não identificado");
                return new ComentarioResult
                {
                    ValidationResult = notFoundResult,
                    Message = "Não encontrado"
                };
            }

            comentario.SetTexto(dto.Texto);

            var updated = _comentarioRepository.Update(comentario);
            await _comentarioRepository.CommitAsync();

            return new ComentarioResult
            {
                Comentario = new ComentarioDto
                {
                    Id = updated.Id,
                    TarefaId = updated.TarefaId,
                    Autor = updated.Autor,
                    Texto = updated.Texto,
                    DataCriacao = updated.DataCriacao
                },
                ValidationResult = validationResult,
                Message = validationResult.IsValid ? "Atualização realizada com sucesso" : "Erro ao atualizar"
            };
        }

        public virtual async Task<ComentarioResult> Delete(int id)
        {
            var comentario = await _comentarioRepository.GetById(id);

            if (comentario == null)
            {
                var notFoundResult = ValidationResultHelper.NotFound("Id", "Não identificado");
                return new ComentarioResult { ValidationResult = notFoundResult };
            }

            _comentarioRepository.Remove(comentario);
            await _comentarioRepository.CommitAsync();
            return new ComentarioResult
            {
                Message = "Removido com sucesso"
            };
        }
    }
}
