﻿using System.Diagnostics.CodeAnalysis;
using Cepedi.Banco.Analise.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cepedi.Banco.Analise.Data;

[ExcludeFromCodeCoverage]
public class ApplicationDbContext : DbContext
{
    // public DbSet<UsuarioEntity> Usuario { get; set; } = default!;
    public DbSet<PessoaCreditoEntity> PessoaCredito { get; set; } = default!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
