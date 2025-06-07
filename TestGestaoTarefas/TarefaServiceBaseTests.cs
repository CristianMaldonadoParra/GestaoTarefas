using Application.Dto.Dtos.Tarefas;
using Domain.Entities;
using Domain.Interfaces.Repository;
using Domain.Services.Tarefas;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace TestGestaoTarefas
{
    public class TarefaServiceBaseTests
    {
        private readonly Mock<ITarefaRepository> _tarefaRepositoryMock;
        private readonly Mock<IValidator<TarefaDto>> _validatorMock;
        private readonly TarefaServiceBase _tarefaService;

        public TarefaServiceBaseTests()
        {
            _tarefaRepositoryMock = new Mock<ITarefaRepository>();
            _validatorMock = new Mock<IValidator<TarefaDto>>();
            _tarefaService = new TarefaServiceBase(_tarefaRepositoryMock.Object, _validatorMock.Object);
        }

        [Fact]
        public async Task GetById_DeveRetornarTarefa_QuandoEncontrada()
        {
            var tarefa = CriarTarefaMock();

            _tarefaRepositoryMock.Setup(r => r.GetById(tarefa.Id)).ReturnsAsync(tarefa);

            var result = await _tarefaService.GetById(tarefa.Id);

            result.Tarefa.Should().NotBeNull();
            result.Tarefa.Id.Should().Be(tarefa.Id);
        }

        [Fact]
        public async Task GetById_DeveRetornarErro_QuandoNaoEncontrada()
        {
            _tarefaRepositoryMock.Setup(r => r.GetById(It.IsAny<int>())).ReturnsAsync((Tarefa)null);

            var result = await _tarefaService.GetById(999);

            result.Tarefa.Should().BeNull();
            result.ValidationResult.Should().NotBeNull();
            result.Message.Should().Be("Não encontrado");
        }

        [Fact]
        public async Task Create_DeveCriarTarefa_QuandoValido()
        {
            var dto = CriarTarefaDtoMock();
            var validationResult = new ValidationResult();

            _validatorMock.Setup(v => v.ValidateAsync(dto, default)).ReturnsAsync(validationResult);

            _tarefaRepositoryMock.Setup(r => r.Add(It.IsAny<Tarefa>())).Returns<Tarefa>(t =>
            {
                t.SetId(10); return t;
            });

            var result = await _tarefaService.Create(dto);

            result.ValidationResult.IsValid.Should().BeTrue();
            result.Tarefa.Id.Should().Be(10);
            result.Message.Should().Be("Criado com sucesso");
            _tarefaRepositoryMock.Verify(r => r.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task Create_DeveRetornarErro_QuandoInvalido()
        {
            var dto = CriarTarefaDtoMock();
            var validationResult = new ValidationResult(new List<ValidationFailure>
            {
                new ValidationFailure("Titulo", "Campo obrigatório")
            });

            _validatorMock.Setup(v => v.ValidateAsync(dto, default)).ReturnsAsync(validationResult);

            var result = await _tarefaService.Create(dto);

            result.ValidationResult.IsValid.Should().BeFalse();
            result.Message.Should().BeNull();
        }

        [Fact]
        public async Task Update_DeveAtualizarTarefa_QuandoValido()
        {
            var dto = CriarTarefaDtoMock();
            var tarefa = CriarTarefaMock();
            var validationResult = new ValidationResult();

            _validatorMock.Setup(v => v.ValidateAsync(dto, default)).ReturnsAsync(validationResult);
            _tarefaRepositoryMock.Setup(r => r.GetById(dto.Id)).ReturnsAsync(tarefa);
            _tarefaRepositoryMock.Setup(r => r.Update(It.IsAny<Tarefa>())).Returns<Tarefa>(t => t);

            var result = await _tarefaService.Update(dto);

            result.ValidationResult.IsValid.Should().BeTrue();
            result.Tarefa.Titulo.Should().Be(dto.Titulo);
            result.Message.Should().Be("Atualização realizada com sucesso");
        }

        [Fact]
        public async Task Update_DeveRetornarErro_QuandoTarefaNaoEncontrada()
        {
            var dto = CriarTarefaDtoMock();
            var validationResult = new ValidationResult();

            _validatorMock.Setup(v => v.ValidateAsync(dto, default)).ReturnsAsync(validationResult);
            _tarefaRepositoryMock.Setup(r => r.GetById(dto.Id)).ReturnsAsync((Tarefa)null);

            var result = await _tarefaService.Update(dto);

            result.Tarefa.Should().BeNull();
            result.Message.Should().Be("Não encontrado");
        }

        [Fact]
        public async Task Delete_DeveRemoverTarefa_QuandoEncontrada()
        {
            var tarefa = CriarTarefaMock();

            _tarefaRepositoryMock.Setup(r => r.GetById(tarefa.Id)).ReturnsAsync(tarefa);

            var result = await _tarefaService.Delete(tarefa.Id);

            result.Message.Should().Be("Removido com sucesso");
            _tarefaRepositoryMock.Verify(r => r.Remove(tarefa), Times.Once);
        }

        [Fact]
        public async Task Delete_DeveRetornarErro_QuandoTarefaNaoExiste()
        {
            _tarefaRepositoryMock.Setup(r => r.GetById(It.IsAny<int>())).ReturnsAsync((Tarefa)null);

            var result = await _tarefaService.Delete(999);

            result.Tarefa.Should().BeNull();
            result.ValidationResult.Should().NotBeNull();
        }

        // Mocks auxiliares
        private Tarefa CriarTarefaMock() =>
            new Tarefa(1, "Titulo teste", "Descrição", DateTime.UtcNow.AddDays(2), 1, 2, DateTime.UtcNow, "admin") { Id = 1 };

        private TarefaDto CriarTarefaDtoMock() => new()
        {
            Id = 1,
            ProjetoId = 1,
            Titulo = "Título",
            Descricao = "Descrição",
            DataVencimento = DateTime.UtcNow.AddDays(5),
            StatusId = 1,
            PrioridadeId = 1,
            DataCriacao = DateTime.UtcNow
        };
    }
}
