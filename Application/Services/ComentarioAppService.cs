using Application.Dto.Dtos.Comentarios;
using Application.Dto.Helper;
using Application.Dto.Mappers;
using Application.Interfaces;
using Domain.Common.Model;
using Domain.Filter.Filters.Comentarios;
using Domain.Interfaces.Services;

namespace Application.Services
{
    public class ComentarioAppService : IComentarioAppService
    {
        private readonly IComentarioService _comentarioService;

        public ComentarioAppService(IComentarioService comentarioService)
        {
            _comentarioService = comentarioService;
        }

        public async Task<ModelResult<ComentarioModel>> GetByIdAsync(int id)
        {
            var result = await _comentarioService.GetById(id);

            var modelResult = new ModelResult<ComentarioModel>
            {
                Model = result.Comentario.MapToModel(),
                Errors = result.ValidationResult?.Errors?.Select(e => e.ErrorMessage).ToList() ?? new List<string>(),
                Message = result.Message
            };
            return modelResult;
        }

        public async Task<PaginateResult<ComentarioModel>> GetWithFilters(ComentarioFilter filters)
        {
            (List<ComentarioDto> Result, int TotalCount) result = await _comentarioService.GetWithFilters(filters);

            var modelResult = new PaginateResult<ComentarioModel>
            {
                ResultData = result.Result.Select(s => s.MapToModel()).ToList(),
                PageSize = filters.PageSize,
                TotalCount = result.TotalCount
            };
            return modelResult;
        }

        public async Task<ModelResult<ComentarioModel>> CreateAsync(ComentarioDto dto)
        {
            var result = await _comentarioService.Create(dto);

            var modelResult = new ModelResult<ComentarioModel>
            {
                Model = result.Comentario?.MapToModel(),
                Message = result.Message,
                Errors = result.ValidationResult?.Errors?.Select(e => e.ErrorMessage).ToList() ?? new List<string>()
            };
            return modelResult;
        }

        public async Task<ModelResult<ComentarioModel>> UpdateAsync(ComentarioDto dto)
        {
            var result = await _comentarioService.Update(dto);

            var modelResult = new ModelResult<ComentarioModel>
            {
                Model = result.Comentario?.MapToModel(),
                Message = result.Message,
                Errors = result.ValidationResult?.Errors?.Select(e => e.ErrorMessage).ToList() ?? new List<string>()
            };
            return modelResult;
        }

        public async Task<ModelResult<ComentarioModel>> DeleteAsync(int id)
        {
            var result = await _comentarioService.Delete(id);

            var modelResult = new ModelResult<ComentarioModel>
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
