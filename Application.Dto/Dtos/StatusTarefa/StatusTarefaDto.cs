using System;

namespace Application.Dto.Dtos.StatusTarefas
{
    public class StatusTarefaDto : DtoBase
    {
        public virtual int Id { get; set; }
        public virtual string Nome { get; set; }
    }
}
