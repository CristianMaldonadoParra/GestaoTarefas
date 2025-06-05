using Application.Dto.Dtos.StatusTarefas;
using Application.Dto.Helper;
using Application.Dto.Mappers;
using Application.Interfaces;
using Domain.Common.Model;
using Domain.Filter.Filters.StatusTarefa;
using Domain.Interfaces.Services;

namespace Application.Services
{
    public class StatusTarefaAppService : IStatusTarefaAppService
    {
        private readonly IStatusTarefaService _statusTarefaService;

        public StatusTarefaAppService(IStatusTarefaService statusTarefaService)
        {
            _statusTarefaService = statusTarefaService;
        }

        public async Task<ModelResult<StatusTarefaModel>> GetByIdAsync(int id)
        {
            var result = await _statusTarefaService.GetById(id);

            var modelResult = new ModelResult<StatusTarefaModel>
            {
                Model = result.StatusTarefa.MapToModel(),
                Errors = result.ValidationResult?.Errors?.Select(e => e.ErrorMessage).ToList() ?? new List<string>(),
                Message = result.Message
            };
            return modelResult;
        }

        public async Task<PaginateResult<StatusTarefaModel>> GetWithFilters(StatusTarefaFilter filters)
        {
            (List<StatusTarefaDto> Result, int TotalCount) result = await _statusTarefaService.GetWithFilters(filters);

            var modelResult = new PaginateResult<StatusTarefaModel>
            {
                ResultData = result.Result.Select(s => s.MapToModel()).ToList(),
                PageSize = filters.PageSize,
                TotalCount = result.TotalCount
            };
            return modelResult;
        }

        public async Task<ModelResult<StatusTarefaIdModel>> CreateAsync(StatusTarefaDto dto)
        {
            var result = await _statusTarefaService.Create(dto);

            var modelResult = new ModelResult<StatusTarefaIdModel>
            {
                Model = result.StatusTarefa?.MapToIdModel(),
                Message = result.Message,
                Errors = result.ValidationResult?.Errors?.Select(e => e.ErrorMessage).ToList() ?? new List<string>()
            };
            return modelResult;
        }

        public async Task<ModelResult<StatusTarefaIdModel>> UpdateAsync(StatusTarefaDto dto)
        {
            var result = await _statusTarefaService.Update(dto);

            var modelResult = new ModelResult<StatusTarefaIdModel>
            {
                Model = result.StatusTarefa?.MapToIdModel(),
                Message = result.Message,
                Errors = result.ValidationResult?.Errors?.Select(e => e.ErrorMessage).ToList() ?? new List<string>()
            };
            return modelResult;
        }

        public async Task<ModelResult<StatusTarefaIdModel>> DeleteAsync(int id)
        {
            var result = await _statusTarefaService.Delete(id);

            var modelResult = new ModelResult<StatusTarefaIdModel>
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
