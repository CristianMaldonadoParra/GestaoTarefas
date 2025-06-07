using Application.Dto.Dtos.HistoricoAtualizacoes;
using Domain.Entities;
using Domain.Interfaces.Repository;
using Domain.Services.HistoricoAtualizacoes;
using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace TestGestaoTarefas
{
    public class HistoricoAtualizacaoServiceBaseTests
    {
        private readonly Mock<IHistoricoAtualizacaoRepository> _repositoryMock;
        private readonly Mock<IValidator<HistoricoAtualizacaoDto>> _validatorMock;
        private readonly HistoricoAtualizacaoServiceBase _service;

        public HistoricoAtualizacaoServiceBaseTests()
        {
            _repositoryMock = new Mock<IHistoricoAtualizacaoRepository>();
            _validatorMock = new Mock<IValidator<HistoricoAtualizacaoDto>>();
            _service = new HistoricoAtualizacaoServiceBase(_repositoryMock.Object, _validatorMock.Object);
        }

        [Fact]
        public async Task GetById_ShouldReturnHistorico_WhenFound()
        {
            var historico = new HistoricoAtualizacao(1, "Autor", "Status", "Antigo", "Novo");
            historico.SetId(1);

            _repositoryMock.Setup(r => r.GetById(1)).ReturnsAsync(historico);

            var result = await _service.GetById(1);

            Assert.NotNull(result.HistoricoAtualizacao);
            Assert.Equal(1, result.HistoricoAtualizacao.Id);
            Assert.Equal("Status", result.HistoricoAtualizacao.CampoAlterado);
        }

        [Fact]
        public async Task GetById_ShouldReturnNotFound_WhenNull()
        {
            _repositoryMock.Setup(r => r.GetById(1)).ReturnsAsync((HistoricoAtualizacao)null!);

            var result = await _service.GetById(1);

            Assert.Null(result.HistoricoAtualizacao);
            Assert.Equal("Não encontrado", result.Message);
            Assert.False(result.ValidationResult.IsValid);
        }

        [Fact]
        public async Task Create_ShouldReturnValidResult_WhenValid()
        {
            var dto = new HistoricoAtualizacaoDto
            {
                TarefaId = 1,
                Autor = "Autor",
                CampoAlterado = "Status",
                ValorAntigo = "Aberto",
                ValorNovo = "Concluído"
            };

            _validatorMock.Setup(v => v.ValidateAsync(dto, default))
                .ReturnsAsync(new ValidationResult());

            var entity = new HistoricoAtualizacao(dto.TarefaId, dto.Autor, dto.CampoAlterado, dto.ValorAntigo, dto.ValorNovo);
            entity.SetId(10);

            _repositoryMock.Setup(r => r.Add(It.IsAny<HistoricoAtualizacao>())).Returns(entity);

            var result = await _service.Create(dto);

            Assert.NotNull(result.HistoricoAtualizacao);
            Assert.Equal(10, result.HistoricoAtualizacao.Id);
            Assert.Equal("Criado com sucesso", result.Message);
            Assert.True(result.ValidationResult.IsValid);
        }

        [Fact]
        public async Task Update_ShouldReturnUpdated_WhenValid()
        {
            var dto = new HistoricoAtualizacaoDto
            {
                Id = 5,
                TarefaId = 1,
                Autor = "Autor",
                CampoAlterado = "Status",
                ValorAntigo = "Aberto",
                ValorNovo = "Fechado",
                DataAlteracao = DateTime.UtcNow
            };

            _validatorMock.Setup(v => v.ValidateAsync(dto, default)).ReturnsAsync(new ValidationResult());

            var entity = new HistoricoAtualizacao(1, "Autor", "Status", "Aberto", "Em andamento");
            entity.SetId(5);

            _repositoryMock.Setup(r => r.GetById(5)).ReturnsAsync(entity);
            _repositoryMock.Setup(r => r.Update(It.IsAny<HistoricoAtualizacao>())).Returns(entity);

            var result = await _service.Update(dto);

            Assert.Equal("Atualização realizada com sucesso", result.Message);
            Assert.True(result.ValidationResult.IsValid);
            Assert.Equal("Fechado", result.HistoricoAtualizacao.ValorNovo);
        }

        [Fact]
        public async Task Delete_ShouldReturnSuccess_WhenFound()
        {
            var entity = new HistoricoAtualizacao(1, "Autor", "Status", "Aberto", "Fechado");
            entity.SetId(5);

            _repositoryMock.Setup(r => r.GetById(5)).ReturnsAsync(entity);

            var result = await _service.Delete(5);

            Assert.Equal("Removido com sucesso", result.Message);
        }

        [Fact]
        public async Task Delete_ShouldReturnNotFound_WhenNull()
        {
            _repositoryMock.Setup(r => r.GetById(99)).ReturnsAsync((HistoricoAtualizacao)null!);

            var result = await _service.Delete(99);

            Assert.False(result.ValidationResult.IsValid);
            Assert.Null(result.HistoricoAtualizacao);
        }
    }
}
