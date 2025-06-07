using Application.Dto.Dtos.Projetos;
using Domain.Entities;
using Domain.Filter.Filters.Projetos;
using Domain.Interfaces.Repository;
using Domain.Services.Projetos;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace TestGestaoTarefas
{
    public class ProjetoServiceBaseTests
    {
        private readonly Mock<IProjetoRepository> _projetoRepositoryMock;
        private readonly Mock<IValidator<ProjetoDto>> _validatorMock;
        private readonly ProjetoServiceBase _projetoService;

        public ProjetoServiceBaseTests()
        {
            _projetoRepositoryMock = new Mock<IProjetoRepository>();
            _validatorMock = new Mock<IValidator<ProjetoDto>>();
            _projetoService = new ProjetoServiceBase(_projetoRepositoryMock.Object, _validatorMock.Object);
        }

        [Fact]
        public async Task GetById_DeveRetornarProjeto_QuandoEncontrado()
        {
            var projeto = CriarProjeto();

            _projetoRepositoryMock.Setup(r => r.GetById(projeto.Id)).ReturnsAsync(projeto);

            var result = await _projetoService.GetById(projeto.Id);

            result.Projeto.Should().NotBeNull();
            result.Projeto.Id.Should().Be(projeto.Id);
        }

        [Fact]
        public async Task GetById_DeveRetornarErro_QuandoNaoEncontrado()
        {
            _projetoRepositoryMock.Setup(r => r.GetById(It.IsAny<int>())).ReturnsAsync((Projeto)null);

            var result = await _projetoService.GetById(999);

            result.Projeto.Should().BeNull();
            result.ValidationResult.Should().NotBeNull();
            result.Message.Should().Be("Não encontrado");
        }

        [Fact]
        public async Task GetWithFilters_DeveRetornarListaDeProjetos()
        {
            var projetos = new List<Projeto>
            {
                CriarProjeto(1),
                CriarProjeto(2)
            };
            var filter = new ProjetosFilter();

            _projetoRepositoryMock.Setup(r => r.GetWithFilters(filter))
                .ReturnsAsync((projetos, projetos.Count));

            var (result, totalCount) = await _projetoService.GetWithFilters(filter);

            result.Should().HaveCount(2);
            totalCount.Should().Be(2);
        }

        [Fact]
        public async Task Create_DeveCriarProjeto_QuandoValido()
        {
            var dto = CriarProjetoDto();
            var validationResult = new ValidationResult();

            _validatorMock.Setup(v => v.ValidateAsync(dto, default)).ReturnsAsync(validationResult);
            _projetoRepositoryMock.Setup(r => r.Add(It.IsAny<Projeto>())).Returns<Projeto>(p =>
            {
                p.SetId(10); return p;
            });

            var result = await _projetoService.Create(dto);

            result.ValidationResult.IsValid.Should().BeTrue();
            result.Projeto.Id.Should().Be(10);
            result.Message.Should().Be("Criado com sucesso");
            _projetoRepositoryMock.Verify(r => r.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task Create_DeveRetornarErro_QuandoInvalido()
        {
            var dto = CriarProjetoDto();
            var validationResult = new ValidationResult(new[] { new ValidationFailure("Nome", "Obrigatório") });

            _validatorMock.Setup(v => v.ValidateAsync(dto, default)).ReturnsAsync(validationResult);

            var result = await _projetoService.Create(dto);

            result.ValidationResult.IsValid.Should().BeFalse();
            result.Projeto.Should().BeNull();
        }

        [Fact]
        public async Task Update_DeveAtualizarProjeto_QuandoValido()
        {
            var dto = CriarProjetoDto();
            var projeto = CriarProjeto();
            var validationResult = new ValidationResult();

            _validatorMock.Setup(v => v.ValidateAsync(dto, default)).ReturnsAsync(validationResult);
            _projetoRepositoryMock.Setup(r => r.GetById(dto.Id)).ReturnsAsync(projeto);
            _projetoRepositoryMock.Setup(r => r.Update(It.IsAny<Projeto>())).Returns<Projeto>(p => p);

            var result = await _projetoService.Update(dto);

            result.Projeto.Should().NotBeNull();
            result.ValidationResult.IsValid.Should().BeTrue();
            result.Message.Should().Be("Atualização realizada com sucesso");
        }

        [Fact]
        public async Task Update_DeveRetornarErro_QuandoNaoEncontrado()
        {
            var dto = CriarProjetoDto();
            var validationResult = new ValidationResult();

            _validatorMock.Setup(v => v.ValidateAsync(dto, default)).ReturnsAsync(validationResult);
            _projetoRepositoryMock.Setup(r => r.GetById(dto.Id)).ReturnsAsync((Projeto)null);

            var result = await _projetoService.Update(dto);

            result.Projeto.Should().BeNull();
            result.Message.Should().Be("Não encontrado");
        }

        [Fact]
        public async Task Delete_DeveRemoverProjeto_QuandoExistente()
        {
            var projeto = CriarProjeto();

            _projetoRepositoryMock.Setup(r => r.GetById(projeto.Id)).ReturnsAsync(projeto);

            var result = await _projetoService.Delete(projeto.Id);

            result.Message.Should().Be("Removido com sucesso");
            _projetoRepositoryMock.Verify(r => r.Remove(projeto), Times.Once);
        }

        [Fact]
        public async Task Delete_DeveRetornarErro_QuandoNaoEncontrado()
        {
            _projetoRepositoryMock.Setup(r => r.GetById(It.IsAny<int>())).ReturnsAsync((Projeto)null);

            var result = await _projetoService.Delete(999);

            result.Projeto.Should().BeNull();
            result.ValidationResult.Should().NotBeNull();
        }

        // Métodos auxiliares

        private Projeto CriarProjeto(int id = 1) =>
            new Projeto("Projeto " + id, "Descrição " + id, DateTime.UtcNow) { Id = id };

        private ProjetoDto CriarProjetoDto() => new ProjetoDto
        {
            Id = 1,
            Nome = "Projeto Teste",
            Descricao = "Descrição Teste",
            DataCriacao = DateTime.UtcNow
        };
    }
}
