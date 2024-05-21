using Serilog;
using Cepedi.Banco.Analise.IoC;
using Cepedi.Banco.Analise.Api;
using Cepedi.Banco.Analise.Dominio.Servicos;
using Refit;

var builder = WebApplication.CreateBuilder(args);
const string URI = "https://localhost:5039";

builder
    .Services
    .AddRefitClient<IExternalBankHistory>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(URI));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.ConfigureAppDependencies(builder.Configuration);

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // await app.InitialiseDatabaseAsync();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHealthChecks("/health");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Map("/", () => Results.Redirect("/swagger"));

app.Run();

