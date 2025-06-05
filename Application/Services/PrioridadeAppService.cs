using Application.Dto.Dtos.Prioridades;
using Application.Dto.Helper;
using Application.Dto.Mappers;
using Application.Interfaces;
using Domain.Common.Model;
using Domain.Filter.Filters.Prioridades;
using Domain.Interfaces.Services;

namespace Application.Services
{
    public class PrioridadeAppService : IPrioridadeAppService
    {
        private readonly IPrioridadeService _prioridadeService;

        public PrioridadeAppService(IPrioridadeService prioridadeService)
        {
            _prioridadeService = prioridadeService;
        }

        public async Task<ModelResult<PrioridadeModel>> GetByIdAsync(int id)
        {
            var result = await _prioridadeService.GetById(id);

            var modelResult = new ModelResult<PrioridadeModel>
            {
                Model = result.Prioridade.MapToModel(),
                Errors = result.ValidationResult?.Errors?.Select(e => e.ErrorMessage).ToList() ?? new List<string>(),
                Message = result.Message
            };
            return modelResult;
        }

        public async Task<PaginateResult<PrioridadeModel>> GetWithFilters(PrioridadeFilter filters)
        {
            (List<PrioridadeDto> Result, int TotalCount) result = await _prioridadeService.GetWithFilters(filters);

            var modelResult = new PaginateResult<PrioridadeModel>
            {
                ResultData = result.Result.Select(s => s.MapToModel()).ToList(),
                PageSize = filters.PageSize,
                TotalCount = result.TotalCount
            };
            return modelResult;
        }

        public async Task<ModelResult<PrioridadeIdModel>> CreateAsync(PrioridadeDto dto)
        {
            var result = await _prioridadeService.Create(dto);

            var modelResult = new ModelResult<PrioridadeIdModel>
            {
                Model = result.Prioridade?.MapToIdModel(),
                Message = result.Message,
                Errors = result.ValidationResult?.Errors?.Select(e => e.ErrorMessage).ToList() ?? new List<string>()
            };
            return modelResult;
        }

        public async Task<ModelResult<PrioridadeIdModel>> UpdateAsync(PrioridadeDto dto)
        {
            var result = await _prioridadeService.Update(dto);

            var modelResult = new ModelResult<PrioridadeIdModel>
            {
                Model = result.Prioridade?.MapToIdModel(),
                Message = result.Message,
                Errors = result.ValidationResult?.Errors?.Select(e => e.ErrorMessage).ToList() ?? new List<string>()
            };
            return modelResult;
        }

        public async Task<ModelResult<PrioridadeIdModel>> DeleteAsync(int id)
        {
            var result = await _prioridadeService.Delete(id);

            var modelResult = new ModelResult<PrioridadeIdModel>
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
