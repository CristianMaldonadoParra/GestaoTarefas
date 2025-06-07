using Application.Dto.Dtos.Comentarios;
using Domain.Entities;
using Domain.Interfaces.Repository;
using Domain.Services.Comentarios;
using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace TestGestaoTarefas
{
    public class ComentarioServiceTests
    {
        private readonly Mock<IComentarioRepository> _comentarioRepoMock;
        private readonly Mock<IValidator<ComentarioDto>> _validatorMock;
        private readonly Mock<IHistoricoAtualizacaoRepository> _historicoRepoMock;
        private readonly ComentarioService _service;

        public ComentarioServiceTests()
        {
            _comentarioRepoMock = new Mock<IComentarioRepository>();
            _validatorMock = new Mock<IValidator<ComentarioDto>>();
            _historicoRepoMock = new Mock<IHistoricoAtualizacaoRepository>();

            _service = new ComentarioService(
                _comentarioRepoMock.Object,
                _validatorMock.Object,
                _historicoRepoMock.Object
            );
        }

        [Fact]
        public async Task Create_ShouldAddHistorico_WhenTextoChanged()
        {
            // Arrange
            var dto = new ComentarioDto { TarefaId = 1, Texto = "Novo texto" };
            var comentarioCriado = new Comentario(dto.TarefaId, "Texto antigo");

            _validatorMock.Setup(v => v.ValidateAsync(dto, default))
                .ReturnsAsync(new ValidationResult());

            _comentarioRepoMock.Setup(r => r.Add(It.IsAny<Comentario>()))
                .Returns(comentarioCriado);

            // Act
            var result = await _service.Create(dto);

            // Assert
            _historicoRepoMock.Verify(h => h.Add(It.Is<HistoricoAtualizacao>(
                h => h.TarefaId == dto.TarefaId &&
                     h.CampoAlterado == "Texto" &&
                     h.ValorAntigo == "Novo texto" && 
                     h.ValorNovo == "Texto antigo"
            )), Times.Once);

            _historicoRepoMock.Verify(h => h.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task Create_ShouldNotAddHistorico_WhenTextoIsSame()
        {
            // Arrange
            var dto = new ComentarioDto { TarefaId = 2, Texto = "Texto igual" };
            var comentarioCriado = new Comentario(dto.TarefaId, "Texto igual");

            _validatorMock.Setup(v => v.ValidateAsync(dto, default))
                .ReturnsAsync(new ValidationResult());

            _comentarioRepoMock.Setup(r => r.Add(It.IsAny<Comentario>()))
                .Returns(comentarioCriado);

            // Act
            var result = await _service.Create(dto);

            // Assert
            _historicoRepoMock.Verify(h => h.Add(It.IsAny<HistoricoAtualizacao>()), Times.Never);
            _historicoRepoMock.Verify(h => h.CommitAsync(), Times.Never);
        }

        [Fact]
        public async Task Create_ShouldNotAddHistorico_WhenValidationFails()
        {
            // Arrange
            var dto = new ComentarioDto { TarefaId = 3, Texto = "Teste" };
            var validationResult = new ValidationResult(new[]
            {
            new ValidationFailure("Texto", "Campo obrigatório")
        });

            _validatorMock.Setup(v => v.ValidateAsync(dto, default))
                .ReturnsAsync(validationResult);

            // Act
            var result = await _service.Create(dto);

            // Assert
            Assert.False(result.ValidationResult.IsValid);
            _historicoRepoMock.Verify(h => h.Add(It.IsAny<HistoricoAtualizacao>()), Times.Never);
            _historicoRepoMock.Verify(h => h.CommitAsync(), Times.Never);
        }
    }
}
