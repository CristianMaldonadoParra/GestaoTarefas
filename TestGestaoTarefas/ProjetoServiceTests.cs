using Application.Dto.Dtos.Projetos;
using Domain.Entities;
using Domain.Filter.Filters.Tarefas;
using Domain.Interfaces.Repository;
using Domain.Services.Projetos;
using FluentValidation;
using Moq;

namespace TestGestaoTarefas
{
    public class ProjetoServiceTests
    {
        private readonly Mock<IProjetoRepository> _projetoRepoMock;
        private readonly Mock<IValidator<ProjetoDto>> _validatorMock;
        private readonly Mock<ITarefaRepository> _tarefaRepoMock;
        private readonly ProjetoService _service;

        public ProjetoServiceTests()
        {
            _projetoRepoMock = new Mock<IProjetoRepository>();
            _validatorMock = new Mock<IValidator<ProjetoDto>>();
            _tarefaRepoMock = new Mock<ITarefaRepository>();

            _service = new ProjetoService(
                _projetoRepoMock.Object,
                _validatorMock.Object,
                _tarefaRepoMock.Object
            );
        }

        [Fact]
        public async Task Delete_ShouldReturnBusinessRuleError_WhenTarefasPendentesExistem()
        {
            // Arrange
            int projetoId = 1;
            _tarefaRepoMock
                .Setup(r => r.GetWithFilters(It.Is<TarefaFilter>(f => f.ProjetoId == projetoId)))
                .ReturnsAsync((new List<Tarefa> { new Tarefa() }, 1));

            // Act
            var result = await _service.Delete(projetoId);

            // Assert
            Assert.NotNull(result.ValidationResult);
            Assert.False(result.ValidationResult.IsValid);
            Assert.Equal("Exclusão não permitida - Para remover um projeto tem que realizar a conclusão ou remoção das tarefas primeiro", result.Message);
            _projetoRepoMock.Verify(r => r.GetById(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task Delete_ShouldCallBaseDelete_WhenNoTarefasPendentes()
        {
            // Arrange
            int projetoId = 2;
            _tarefaRepoMock
                .Setup(r => r.GetWithFilters(It.Is<TarefaFilter>(f => f.ProjetoId == projetoId)))
                .ReturnsAsync((new List<Tarefa>(), 0));

            var projeto = new Projeto("Projeto Teste", "Descrição teste", DateTime.UtcNow);
            
            _projetoRepoMock.Setup(r => r.GetById(projetoId)).ReturnsAsync(projeto);
            _projetoRepoMock.Setup(r => r.Remove(projeto));
            _projetoRepoMock.Setup(r => r.CommitAsync()).ReturnsAsync(1);

            // Act
            var result = await _service.Delete(projetoId);

            // Assert
            Assert.True(result.ValidationResult == null || result.ValidationResult.IsValid);
            Assert.Equal("Removido com sucesso", result.Message);
            _projetoRepoMock.Verify(r => r.Remove(It.IsAny<Projeto>()), Times.Once);
            _projetoRepoMock.Verify(r => r.CommitAsync(), Times.Once);
        }
    }
}
