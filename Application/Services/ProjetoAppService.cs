using Application.Dto.Dtos.Projetos;
using Application.Dto.Helper;
using Application.Dto.Mappers;
using Application.Interfaces;
using Domain.Common.Model;
using Domain.Filter.Filters.Projetos;
using Domain.Interfaces.Services;

namespace Application.Services
{
    public class ProjetoAppService : IProjetoAppService
    {
        private readonly IProjetoService _projetoService;

        public ProjetoAppService(IProjetoService projetoService)
        {
            _projetoService = projetoService;
        }

        public async Task<ModelResult<ProjetoModel>> GetByIdAsync(int id)
        {
            var result = await _projetoService.GetById(id);

            var modelResult = new ModelResult<ProjetoModel>
            {
                Model = result.Projeto.MapToModel(),
                Errors = result.ValidationResult?.Errors?.Select(e => e.ErrorMessage).ToList() ?? new List<string>(),
                Message = result.Message
            };
            return modelResult;
        }

        public async Task<PaginateResult<ProjetoModel>> GetWithFilters(ProjetosFilter filters)
        {
            (List<ProjetoDto> Result, int TotalCount) result = await _projetoService.GetWithFilters(filters);

            var modelResult = new PaginateResult<ProjetoModel>
            {
                ResultData = result.Result.Select(s => s.MapToModel()).ToList(),
                PageSize = filters.PageSize,
                TotalCount = result.TotalCount
            };
            return modelResult;
        }

        public async Task<ModelResult<ProjetoIdModel>> CreateAsync(ProjetoDto dto)
        {
            var result = await _projetoService.Create(dto);

            var modelResult = new ModelResult<ProjetoIdModel>
            {
                Model = result.Projeto?.MapToIdModel(),
                Message = result.Message,
                Errors = result.ValidationResult?.Errors?.Select(e => e.ErrorMessage).ToList() ?? new List<string>()
            };
            return modelResult;
        }

        public async Task<ModelResult<ProjetoIdModel>> UpdateAsync(ProjetoDto dto)
        {
            var result = await _projetoService.Update(dto);

            var modelResult = new ModelResult<ProjetoIdModel>
            {
                Model = result.Projeto?.MapToIdModel(),
                Message = result.Message,
                Errors = result.ValidationResult?.Errors?.Select(e => e.ErrorMessage).ToList() ?? new List<string>()
            };
            return modelResult;
        }

        public async Task<ModelResult<ProjetoIdModel>> DeleteAsync(int id)
        {
            var result = await _projetoService.Delete(id);

            var modelResult = new ModelResult<ProjetoIdModel>
            {
                Message = result.Message,
                Errors = result.ValidationResult?.Errors?.Select(e => e.ErrorMessage).ToList() ?? new List<string>()
            };
            return modelResult;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
