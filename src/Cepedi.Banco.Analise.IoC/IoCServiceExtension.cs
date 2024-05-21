using System.Diagnostics.CodeAnalysis;
using Cepedi.Banco.Analise.Compartilhado;
using Cepedi.Banco.Analise.Dados;
using Cepedi.Banco.Analise.Dados.Repositories;
using Cepedi.Banco.Analise.Dominio.Pipelines;
using Cepedi.Banco.Analise.Dominio.Repositorio;
using FluentValidation;
using MediatR;
using Refit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Cepedi.Banco.Analise.Dominio.Servicos;

namespace Cepedi.Banco.Analise.IoC
{
    [ExcludeFromCodeCoverage]
    public static class IoCServiceExtension
    {
        public static void ConfigureAppDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            ConfigureDbContext(services, configuration);
            services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ExcecaoPipeline<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidacaoComportamento<,>));
            ConfigurarFluentValidation(services);
            services.AddScoped<IPessoaCreditoRepository, PessoaCreditoRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddHealthChecks()
                .AddDbContextCheck<ApplicationDbContext>();
            ConfigurarRefit(services, configuration);
        }
        private static void ConfigurarRefit(IServiceCollection services, IConfiguration configuration)
        {
            
            services.
                AddRefitClient<IExternalBankHistory>().
                ConfigureHttpClient(httpClient =>
                    httpClient.BaseAddress =
                    new Uri("http://localhost:5039")
                );
        }
        private static void ConfigurarFluentValidation(IServiceCollection services)
        {
            var abstractValidator = typeof(AbstractValidator<>);
            var validadores = typeof(Entrada)
                .Assembly
                .DefinedTypes
                .Where(type => type.BaseType?.IsGenericType is true &&
                type.BaseType.GetGenericTypeDefinition() ==
                abstractValidator)
                .Select(Activator.CreateInstance)
                .ToArray();

            foreach (var validator in validadores)
            {
                services.AddSingleton(validator!.GetType().BaseType!, validator);
            }
        }

        private static void ConfigureDbContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                //options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<ApplicationDbContextInitialiser>();
        }
    }
}
