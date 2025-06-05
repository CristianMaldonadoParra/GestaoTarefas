using Application.Dto.Dtos.Comentarios;
using Application.Dto.Dtos.HistoricoAtualizacoes;
using Application.Dto.Dtos.Prioridades;
using Application.Dto.Dtos.Projetos;
using Application.Dto.Dtos.StatusTarefas;
using Application.Dto.Dtos.Tarefas;
using Application.Interfaces;
using Application.Services;
using Application.Validators;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Services;
using Domain.Services.Comentarios;
using Domain.Services.HistoricoAtualizacoes;
using Domain.Services.Prioridades;
using Domain.Services.Projetos;
using Domain.Services.StatusTarefas;
using Domain.Services.Tarefas;
using FluentValidation;
using Infrastructure.Data.Repository.Comentarios;
using Infrastructure.Data.Repository.HistoricoAtualizacoes;
using Infrastructure.Data.Repository.Prioridades;
using Infrastructure.Data.Repository.Projetos;
using Infrastructure.Data.Repository.StatusTarefas;
using Infrastructure.Data.Repository.Tarefas;

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

            builder.Services.AddScoped<ITarefaAppService, TarefaAppService>();
            builder.Services.AddScoped<ITarefaService, TarefaService>();
            builder.Services.AddScoped<ITarefaRepository, TarefaRepository>();
            builder.Services.AddScoped<IValidator<TarefaDto>, TarefaDtoValidator>();

            builder.Services.AddScoped<IComentarioAppService, ComentarioAppService>();
            builder.Services.AddScoped<IComentarioService, ComentarioService>();
            builder.Services.AddScoped<IComentarioRepository, ComentarioRepository>();
            builder.Services.AddScoped<IValidator<ComentarioDto>, ComentarioDtoValidator>();

            builder.Services.AddScoped<IHistoricoAtualizacaoAppService, HistoricoAtualizacaoAppService>();
            builder.Services.AddScoped<IHistoricoAtualizacaoService, HistoricoAtualizacaoServiceBase>();
            builder.Services.AddScoped<IHistoricoAtualizacaoRepository, HistoricoAtualizacaoRepository>();
            builder.Services.AddScoped<IValidator<HistoricoAtualizacaoDto>, HistoricoAtualizacaoDtoValidator>();


            return builder;
        }
    }
}
