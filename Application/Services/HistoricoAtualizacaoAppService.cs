using Application.Dto.Dtos.HistoricoAtualizacoes;
using Application.Dto.Helper;
using Application.Dto.Mappers;
using Application.Interfaces;
using Domain.Common.Model;
using Domain.Filter.Filters.HistoricoAtualizacoes;
using Domain.Interfaces.Services;

namespace Application.Services
{
    public class HistoricoAtualizacaoAppService : IHistoricoAtualizacaoAppService
    {
        private readonly IHistoricoAtualizacaoService _service;

        public HistoricoAtualizacaoAppService(IHistoricoAtualizacaoService service)
        {
            _service = service;
        }

        public async Task<ModelResult<HistoricoAtualizacaoModel>> GetByIdAsync(int id)
        {
            var result = await _service.GetById(id);

            var modelResult = new ModelResult<HistoricoAtualizacaoModel>
            {
                Model = result.HistoricoAtualizacao.MapToModel(),
                Errors = result.ValidationResult?.Errors?.Select(e => e.ErrorMessage).ToList() ?? new List<string>(),
                Message = result.Message
            };
            return modelResult;
        }

        public async Task<PaginateResult<HistoricoAtualizacaoModel>> GetWithFilters(HistoricoAtualizacaoFilter filters)
        {
            (List<HistoricoAtualizacaoDto> Result, int TotalCount) result = await _service.GetWithFilters(filters);

            var modelResult = new PaginateResult<HistoricoAtualizacaoModel>
            {
                ResultData = result.Result.Select(s => s.MapToModel()).ToList(),
                PageSize = filters.PageSize,
                TotalCount = result.TotalCount
            };
            return modelResult;
        }

        public async Task<ModelResult<HistoricoAtualizacaoModel>> CreateAsync(HistoricoAtualizacaoDto dto)
        {
            var result = await _service.Create(dto);

            var modelResult = new ModelResult<HistoricoAtualizacaoModel>
            {
                Model = result.HistoricoAtualizacao?.MapToModel(),
                Message = result.Message,
                Errors = result.ValidationResult?.Errors?.Select(e => e.ErrorMessage).ToList() ?? new List<string>()
            };
            return modelResult;
        }

        public async Task<ModelResult<HistoricoAtualizacaoModel>> UpdateAsync(HistoricoAtualizacaoDto dto)
        {
            var result = await _service.Update(dto);

            var modelResult = new ModelResult<HistoricoAtualizacaoModel>
            {
                Model = result.HistoricoAtualizacao?.MapToModel(),
                Message = result.Message,
                Errors = result.ValidationResult?.Errors?.Select(e => e.ErrorMessage).ToList() ?? new List<string>()
            };
            return modelResult;
        }

        public async Task<ModelResult<HistoricoAtualizacaoModel>> DeleteAsync(int id)
        {
            var result = await _service.Delete(id);

            var modelResult = new ModelResult<HistoricoAtualizacaoModel>
            {
                Message = result.Message,
                Errors = result.ValidationResult?.Errors?.Select(e => e.ErrorMessage).ToList() ?? new List<string>()
            };
            return modelResult;
        }

        public void Dispose()
        {
        }
    }
}
