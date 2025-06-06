using Application.Dto.Helper;
using Domain.Entities;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Services;
using FluentValidation;
using Application.Dto.Dtos.Projetos;
using Domain.Filter.Filters.Projetos;

namespace Domain.Services.Projetos
{
    public class ProjetoServiceBase : IProjetoService
    {
        private readonly IProjetoRepository _projetosRepository;        
        private readonly IValidator<ProjetoDto> _validator;

        public ProjetoServiceBase(IProjetoRepository projetosRepository, IValidator<ProjetoDto> validator)
        {
            _projetosRepository = projetosRepository;
            _validator = validator;
        }

        public virtual async Task<ProjetoResult> GetById(int id)
        {
            var projeto = await _projetosRepository.GetById(id);
            if (projeto == null)
            {
                var notFoundResult = ValidationResultHelper.NotFound("Id", "Não identificado");
                return new ProjetoResult
                {
                    ValidationResult = notFoundResult,
                    Message = "Não encontrado"
                };
            }

            return new ProjetoResult
            {
                Projeto = new ProjetoDto
                {
                    Id = projeto.Id,
                    Nome = projeto.Nome,
                    Descricao = projeto.Descricao,
                    DataCriacao = projeto.DataCriacao
                }
            };
        }

        public virtual async Task<(List<ProjetoDto> Result, int TotalCount)> GetWithFilters(ProjetosFilter filter)
        {
            (IEnumerable<Projeto> Result, int TotalCount) result = await _projetosRepository.GetWithFilters(filter);
            return (result.Result.Select(p => new ProjetoDto
            {
                Id = p.Id,
                Nome = p.Nome,
                Descricao = p.Descricao,
                DataCriacao = p.DataCriacao
            }).ToList(), result.TotalCount);
        }

        public virtual async Task<ProjetoResult> Create(ProjetoDto dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return new ProjetoResult { ValidationResult = validationResult };

            var projeto = new Projeto(dto.Nome, dto.Descricao, dto.DataCriacao);
            var result = _projetosRepository.Add(projeto);
            await _projetosRepository.CommitAsync();

            return new ProjetoResult
            {
                Projeto = new ProjetoDto
                {
                    Id = result.Id,
                    Nome = result.Nome,
                    Descricao = result.Descricao,
                    DataCriacao = result.DataCriacao
                },
                ValidationResult = validationResult,
                Message = validationResult.IsValid ? "Criado com sucesso" : "Erro ao criar"
            };
        }

        public virtual async Task<ProjetoResult> Update(ProjetoDto dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return new ProjetoResult { ValidationResult = validationResult };

            var projeto = await _projetosRepository.GetById(dto.Id);
            if (projeto == null)
            {
                var notFoundResult = ValidationResultHelper.NotFound(nameof(dto.Id), "Não identificado");
                return new ProjetoResult
                {
                    ValidationResult = notFoundResult,
                    Message = "Não encontrado"
                };
            }

            projeto.SetNome(dto.Nome);
            projeto.SetDescricao(dto.Descricao);
            projeto.SetDataCriacao(dto.DataCriacao);

            var updated = _projetosRepository.Update(projeto);
            await _projetosRepository.CommitAsync();

            return new ProjetoResult
            {
                Projeto = new ProjetoDto
                {
                    Id = updated.Id,
                    Nome = updated.Nome,
                    Descricao = updated.Descricao,
                    DataCriacao = updated.DataCriacao
                },
                ValidationResult = validationResult,
                Message = validationResult.IsValid ? "Atualização realizada com sucesso" : "Erro ao atualizar"
            };
        }

        public virtual async Task<ProjetoResult> Delete(int id)
        {
            var projeto = await _projetosRepository.GetById(id);

            if (projeto == null)
            {
                var notFoundResult = ValidationResultHelper.NotFound("Id", "Não identificado");
                return new ProjetoResult { ValidationResult = notFoundResult };
            }

            _projetosRepository.Remove(projeto);
            await _projetosRepository.CommitAsync();
            return new ProjetoResult
            {
                Message = "Removido com sucesso"
            };
        }
    }
}
