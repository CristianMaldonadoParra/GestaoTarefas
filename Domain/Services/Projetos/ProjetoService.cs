using Application.Dto.Dtos.Projetos;
using Application.Dto.Helper;
using Domain.Enums.Enums;
using Domain.Filter.Filters.Tarefas;
using Domain.Interfaces.Repository;
using FluentValidation;

namespace Domain.Services.Projetos
{

    public class ProjetoService : ProjetoServiceBase
    {
        private readonly ITarefaRepository _tarefaRepository;
        public ProjetoService(IProjetoRepository projetoRepository, IValidator<ProjetoDto> validator, ITarefaRepository tarefaRepository) : base(projetoRepository, validator)
        {
            _tarefaRepository = tarefaRepository;
        }

        public override async Task<ProjetoResult> Delete(int id)
        {
            var tarefasPendentes = await _tarefaRepository.GetWithFilters(new TarefaFilter { ProjetoId = id, StatusId = (int)EStatusTarefa.Pendente, IsPagination = false });
            
            if(tarefasPendentes.TotalCount > 0)
            {
                var validationResult = ValidationResultHelper.BusinessRule("Projeto", "Não é possível excluir o projeto, pois existem tarefas pendentes associadas.");
                return new ProjetoResult
                {
                    ValidationResult = validationResult,
                    Message = "Exclusão não permitida - Para remover um projeto tem que realizar a conclusão ou remoção das tarefas primeiro"
                };
            }


            return await base.Delete(id);
        }
    }
}
