using Application.Dto.Dtos.Prioridades;
using Application.Dto.Dtos.Projetos;
using Application.Dto.Dtos.StatusTarefas;
using Application.Interfaces;
using Application.Services;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Services;
using Domain.Services.Prioridades;
using Domain.Services.Projetos;
using Domain.Services.StatusTarefas;
using FluentValidation;
using Infrastructure.Data.Repository.Prioridades;
using Infrastructure.Data.Repository.Projetos;
using Infrastructure.Data.Repository.StatusTarefas;

namespace Api.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static WebApplicationBuilder AddDependencyInjectionConfiguration(this WebApplicationBuilder builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            builder.Services.AddScoped<IProjetoAppService, ProjetoAppService>();
            builder.Services.AddScoped<IProjetoService, ProjetoService>();
            builder.Services.AddScoped<IProjetoRepository, ProjetoRepository>();
            builder.Services.AddScoped<IValidator<ProjetoDto>, ProjetoDtoValidator>();

            builder.Services.AddScoped<IStatusTarefaAppService, StatusTarefaAppService>();
            builder.Services.AddScoped<IStatusTarefaService, StatusTarefaService>();
            builder.Services.AddScoped<IStatusTarefaRepository, StatusTarefaRepository>();
            builder.Services.AddScoped<IValidator<StatusTarefaDto>, StatusTarefaDtoValidator>();

            builder.Services.AddScoped<IPrioridadeAppService, PrioridadeAppService>();
            builder.Services.AddScoped<IPrioridadeService, PrioridadeService>();
            builder.Services.AddScoped<IPrioridadeRepository, PrioridadeRepository>();
            builder.Services.AddScoped<IValidator<PrioridadeDto>, PrioridadeDtoValidator>();

            return builder;
        }
    }
}
