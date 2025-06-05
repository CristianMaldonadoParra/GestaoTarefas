using System;

namespace Application.Dto.Dtos.Projetos
{
    public class ProjetoDto : DtoBase
    {
        public virtual int Id { get; set; }
        public virtual string Nome { get; set; }
        public virtual string Descricao { get; set; }
        public virtual DateTime DataCriacao { get; set; }
    }
}
