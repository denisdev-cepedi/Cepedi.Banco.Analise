using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Cepedi.Banco.Analise.Data;
using Cepedi.Banco.Analise.Data.Repositories;
using Cepedi.Banco.Analise.Domain;
using Cepedi.Banco.Analise.Domain.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cepedi.Banco.Analise.IoC
{
    [ExcludeFromCodeCoverage]
    public static class AppServiceCollectionExtension
    {
        public static void ConfigureAppDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            ConfigureDbContext(services, configuration);
            services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
            //services.AddMediatR(new[] { typeof(IDomainEntryPoint).Assembly });

            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            //services.AddHttpContextAccessor();

            services.AddHealthChecks()
                .AddDbContextCheck<ApplicationDbContext>();
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
