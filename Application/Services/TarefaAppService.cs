using Application.Dto.Dtos.Tarefas;
using Application.Dto.Helper;
using Application.Dto.Mappers;
using Application.Interfaces;
using Domain.Common.Model;
using Domain.Filter.Filters.Tarefas;
using Domain.Interfaces.Services;

namespace Application.Services
{
    public class TarefaAppService : ITarefaAppService
    {
        private readonly ITarefaService _tarefaService;

        public TarefaAppService(ITarefaService tarefaService)
        {
            _tarefaService = tarefaService;
        }

        public async Task<ModelResult<TarefaModel>> GetByIdAsync(int id)
        {
            var result = await _tarefaService.GetById(id);

            var modelResult = new ModelResult<TarefaModel>
            {
                Model = result.Tarefa.MapToModel(),
                Errors = result.ValidationResult?.Errors?.Select(e => e.ErrorMessage).ToList() ?? new List<string>(),
                Message = result.Message
            };
            return modelResult;
        }

        public async Task<PaginateResult<TarefaModel>> GetWithFilters(TarefaFilter filters)
        {
            (List<TarefaDto> Result, int TotalCount) result = await _tarefaService.GetWithFilters(filters);

            var modelResult = new PaginateResult<TarefaModel>
            {
                ResultData = result.Result.Select(s => s.MapToModel()).ToList(),
                PageSize = filters.PageSize,
                TotalCount = result.TotalCount
            };
            return modelResult;
        }

        public async Task<ModelResult<TarefaIdModel>> CreateAsync(TarefaDto dto)
        {
            var result = await _tarefaService.Create(dto);

            var modelResult = new ModelResult<TarefaIdModel>
            {
                Model = result.Tarefa?.MapToIdModel(),
                Message = result.Message,
                Errors = result.ValidationResult?.Errors?.Select(e => e.ErrorMessage).ToList() ?? new List<string>()
            };
            return modelResult;
        }

        public async Task<ModelResult<TarefaIdModel>> UpdateAsync(TarefaDto dto)
        {
            var result = await _tarefaService.Update(dto);

            var modelResult = new ModelResult<TarefaIdModel>
            {
                Model = result.Tarefa?.MapToIdModel(),
                Message = result.Message,
                Errors = result.ValidationResult?.Errors?.Select(e => e.ErrorMessage).ToList() ?? new List<string>()
            };
            return modelResult;
        }

        public async Task<ModelResult<TarefaIdModel>> DeleteAsync(int id)
        {
            var result = await _tarefaService.Delete(id);

            var modelResult = new ModelResult<TarefaIdModel>
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
