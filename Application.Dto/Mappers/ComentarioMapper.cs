using Application.Dto.Dtos.Comentarios;

namespace Application.Dto.Mappers
{
    public static class ComentarioMapper
    {
        public static ComentarioModel MapToModel(this ComentarioDto dto)
        {
            if (dto == null) return null;
            return new ComentarioModel
            {
                Id = dto.Id,
                TarefaId = dto.TarefaId,
                Autor = dto.Autor,
                Texto = dto.Texto,
                DataCriacao = dto.DataCriacao
            };
        }

        public static ComentarioDto MapToDto(this ComentarioModel model)
        {
            if (model == null) return null;
            return new ComentarioDto
            {
                Id = model.Id,
                TarefaId = model.TarefaId,
                Autor = model.Autor,
                Texto = model.Texto,
                DataCriacao = model.DataCriacao
            };
        }
    }
}
