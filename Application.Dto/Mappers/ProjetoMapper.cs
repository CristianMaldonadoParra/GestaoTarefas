using Application.Dto.Dtos.Projetos;

namespace Application.Dto.Mappers
{
    public static class ProjetoMapper
    {
        public static ProjetoIdModel MapToIdModel(this ProjetoDto dto)
        {
            if (dto == null) return null;

            return new ProjetoIdModel
            {
                Id = dto.Id
            };
        }

        public static ProjetoModel MapToModel(this ProjetoDto dto)
        {
            if (dto == null) return null;

            return new ProjetoModel
            {
                Id = dto.Id,
                Nome = dto.Nome,
                Descricao = dto.Descricao,
                DataCriacao = dto.DataCriacao
            };
        }

        public static ProjetoDto MapToDto(this ProjetoModel model)
        {
            if (model == null) return null;

            return new ProjetoDto
            {
                Id = model.Id,
                Nome = model.Nome,
                Descricao = model.Descricao,
                DataCriacao = model.DataCriacao
            };
        }
    }
}
