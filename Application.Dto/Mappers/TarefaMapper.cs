using Application.Dto.Dtos.Tarefas;

namespace Application.Dto.Mappers
{
    public static class TarefaMapper
    {
        public static TarefaIdModel MapToIdModel(this TarefaDto dto)
        {
            if (dto == null) return null;

            return new TarefaIdModel
            {
                Id = dto.Id
            };
        }

        public static TarefaModel MapToModel(this TarefaDto dto)
        {
            if (dto == null) return null;

            return new TarefaModel
            {
                Id = dto.Id,
                ProjetoId = dto.ProjetoId,
                Titulo = dto.Titulo,
                Descricao = dto.Descricao,
                DataVencimento = dto.DataVencimento,
                StatusId = dto.StatusId,
                PrioridadeId = dto.PrioridadeId,
                DataCriacao = dto.DataCriacao
            };
        }

        public static TarefaDto MapToDto(this TarefaModel model)
        {
            if (model == null) return null;

            return new TarefaDto
            {
                Id = model.Id,
                ProjetoId = model.ProjetoId,
                Titulo = model.Titulo,
                Descricao = model.Descricao,
                DataVencimento = model.DataVencimento,
                StatusId = model.StatusId,
                PrioridadeId = model.PrioridadeId,
                DataCriacao = model.DataCriacao
            };
        }
    }
}
