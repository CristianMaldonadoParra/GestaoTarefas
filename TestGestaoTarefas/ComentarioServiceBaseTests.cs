using Application.Dto.Dtos.Comentarios;
using Domain.Constant;
using Domain.Entities;
using Domain.Filter.Filters.Comentarios;
using Domain.Interfaces.Repository;
using Domain.Services.Comentarios;
using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace TestGestaoTarefas
{
    public class ComentarioServiceBaseTests
    {
        private readonly Mock<IComentarioRepository> _comentarioRepositoryMock;
        private readonly Mock<IValidator<ComentarioDto>> _validatorMock;
        private readonly ComentarioServiceBase _service;

        public ComentarioServiceBaseTests()
        {
            _comentarioRepositoryMock = new Mock<IComentarioRepository>();
            _validatorMock = new Mock<IValidator<ComentarioDto>>();
            _service = new ComentarioServiceBase(_comentarioRepositoryMock.Object, _validatorMock.Object);
        }

        [Fact]
        public async Task GetById_ShouldReturnComentario_WhenFound()
        {
            var comentario = new Comentario(1, "Texto") { Id = 10 };
            comentario.SetAutor("Usuario Gerente");

            _comentarioRepositoryMock.Setup(r => r.GetById(10)).ReturnsAsync(comentario);

            var result = await _service.GetById(10);

            Assert.NotNull(result.Comentario);
            Assert.Equal(10, result.Comentario.Id);
            Assert.Equal("Texto", result.Comentario.Texto);
        }

        [Fact]
        public async Task GetById_ShouldReturnNotFound_WhenNull()
        {
            _comentarioRepositoryMock.Setup(r => r.GetById(1)).ReturnsAsync((Comentario)null);

            var result = await _service.GetById(1);

            Assert.Null(result.Comentario);
            Assert.False(result.ValidationResult.IsValid);
            Assert.Equal("Não encontrado", result.Message);
        }

        [Fact]
        public async Task GetWithFilters_ShouldReturnListAndTotal()
        {
            var filter = new ComentarioFilter();
            var comentarios = new List<Comentario> { new Comentario(1, "Teste") };
            comentarios[0].SetAutor("Autor");

            _comentarioRepositoryMock
                .Setup(r => r.GetWithFilters(filter))
                .ReturnsAsync((comentarios, comentarios.Count));

            var (result, total) = await _service.GetWithFilters(filter);

            Assert.Single(result);
            Assert.Equal(comentarios.Count, total);
        }

        [Fact]
        public async Task Create_ShouldReturnValidationErrors_WhenInvalid()
        {
            var dto = new ComentarioDto { Texto = "" };
            var validationResult = new ValidationResult(new[] { new ValidationFailure("Texto", "Erro") });

            _validatorMock.Setup(v => v.ValidateAsync(dto, default)).ReturnsAsync(validationResult);

            var result = await _service.Create(dto);

            Assert.False(result.ValidationResult.IsValid);
            Assert.Null(result.Comentario);
        }

        [Fact]
        public async Task Create_ShouldAddComentario_WhenValid()
        {
            var dto = new ComentarioDto { TarefaId = 1, Texto = "Comentário" };
            var validationResult = new ValidationResult();

            var comentario = new Comentario(1, "Comentário");
            comentario.SetAutor(UsuarioServiceExternoConst.UserName);

            _validatorMock.Setup(v => v.ValidateAsync(dto, default)).ReturnsAsync(validationResult);
            _comentarioRepositoryMock.Setup(r => r.Add(It.IsAny<Comentario>())).Returns(comentario);

            var result = await _service.Create(dto);

            Assert.True(result.ValidationResult.IsValid);
            Assert.NotNull(result.Comentario);
            Assert.Equal("Comentário", result.Comentario.Texto);
        }

        [Fact]
        public async Task Update_ShouldReturnValidationErrors_WhenInvalid()
        {
            var dto = new ComentarioDto { Id = 1, Texto = "" };
            var validationResult = new ValidationResult(new[] { new ValidationFailure("Texto", "Erro") });

            _validatorMock.Setup(v => v.ValidateAsync(dto, default)).ReturnsAsync(validationResult);

            var result = await _service.Update(dto);

            Assert.False(result.ValidationResult.IsValid);
        }

        [Fact]
        public async Task Update_ShouldReturnNotFound_WhenComentarioDoesNotExist()
        {
            var dto = new ComentarioDto { Id = 1, Texto = "Novo Texto" };
            var validationResult = new ValidationResult();

            _validatorMock.Setup(v => v.ValidateAsync(dto, default)).ReturnsAsync(validationResult);
            _comentarioRepositoryMock.Setup(r => r.GetById(dto.Id)).ReturnsAsync((Comentario)null);

            var result = await _service.Update(dto);

            Assert.Null(result.Comentario);
            Assert.Equal("Não encontrado", result.Message);
        }

        [Fact]
        public async Task Update_ShouldModifyComentario_WhenValid()
        {
            var dto = new ComentarioDto { Id = 1, Texto = "Novo Texto" };
            var validationResult = new ValidationResult();
            var comentario = new Comentario(1, "Texto Antigo");

            _validatorMock.Setup(v => v.ValidateAsync(dto, default)).ReturnsAsync(validationResult);
            _comentarioRepositoryMock.Setup(r => r.GetById(dto.Id)).ReturnsAsync(comentario);
            _comentarioRepositoryMock.Setup(r => r.Update(It.IsAny<Comentario>())).Returns(comentario);

            var result = await _service.Update(dto);

            Assert.NotNull(result.Comentario);
            Assert.Equal("Novo Texto", result.Comentario.Texto);
        }

        [Fact]
        public async Task Delete_ShouldReturnNotFound_WhenNotExists()
        {
            _comentarioRepositoryMock.Setup(r => r.GetById(1)).ReturnsAsync((Comentario)null);

            var result = await _service.Delete(1);

            Assert.False(result.ValidationResult.IsValid);
        }

        [Fact]
        public async Task Delete_ShouldRemoveComentario_WhenExists()
        {
            var comentario = new Comentario(1, "Texto");

            _comentarioRepositoryMock.Setup(r => r.GetById(1)).ReturnsAsync(comentario);

            var result = await _service.Delete(1);

            Assert.Equal("Removido com sucesso", result.Message);
        }
    }
}
