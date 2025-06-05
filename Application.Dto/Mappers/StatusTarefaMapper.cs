using Application.Dto.Dtos.StatusTarefas;

namespace Application.Dto.Mappers
{
    public static class StatusTarefaMapper
    {
        public static StatusTarefaIdModel MapToIdModel(this StatusTarefaDto dto)
        {
            if (dto == null) return null;

            return new StatusTarefaIdModel
            {
                Id = dto.Id
            };
        }

        public static StatusTarefaModel MapToModel(this StatusTarefaDto dto)
        {
            if (dto == null) return null;

            return new StatusTarefaModel
            {
                Id = dto.Id,
                Nome = dto.Nome
            };
        }

        public static StatusTarefaDto MapToDto(this StatusTarefaModel model)
        {
            if (model == null) return null;

            return new StatusTarefaDto
            {
                Id = model.Id,
                Nome = model.Nome
            };
        }
    }
}
