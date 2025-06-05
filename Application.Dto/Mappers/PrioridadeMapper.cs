using Application.Dto.Dtos.Prioridades;

namespace Application.Dto.Mappers
{
    public static class PrioridadeMapper
    {
        public static PrioridadeIdModel MapToIdModel(this PrioridadeDto dto)
        {
            if (dto == null) return null;

            return new PrioridadeIdModel
            {
                Id = dto.Id
            };
        }

        public static PrioridadeModel MapToModel(this PrioridadeDto dto)
        {
            if (dto == null) return null;

            return new PrioridadeModel
            {
                Id = dto.Id,
                Nome = dto.Nome
            };
        }

        public static PrioridadeDto MapToDto(this PrioridadeModel model)
        {
            if (model == null) return null;

            return new PrioridadeDto
            {
                Id = model.Id,
                Nome = model.Nome
            };
        }
    }
}
