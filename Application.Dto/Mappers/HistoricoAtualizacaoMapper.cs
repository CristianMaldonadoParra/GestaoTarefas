using Application.Dto.Dtos.HistoricoAtualizacoes;

namespace Application.Dto.Mappers
{
    public static class HistoricoAtualizacaoMapper
    {
        public static HistoricoAtualizacaoModel MapToModel(this HistoricoAtualizacaoDto dto)
        {
            if (dto == null) return null;
            return new HistoricoAtualizacaoModel
            {
                Id = dto.Id,
                TarefaId = dto.TarefaId,
                Autor = dto.Autor,
                CampoAlterado = dto.CampoAlterado,
                ValorAntigo = dto.ValorAntigo,
                ValorNovo = dto.ValorNovo,
                DataAlteracao = dto.DataAlteracao
            };
        }

        public static HistoricoAtualizacaoDto MapToDto(this HistoricoAtualizacaoModel model)
        {
            if (model == null) return null;
            return new HistoricoAtualizacaoDto
            {
                Id = model.Id,
                TarefaId = model.TarefaId,
                Autor = model.Autor,
                CampoAlterado = model.CampoAlterado,
                ValorAntigo = model.ValorAntigo,
                ValorNovo = model.ValorNovo,
                DataAlteracao = model.DataAlteracao
            };
        }
    }
}
