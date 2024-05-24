using Cepedi.Banco.Analise.Compartilhado.Dtos;
using Cepedi.Banco.Analise.Dominio.Entidades;
using Cepedi.Banco.Analise.Dominio.Repositorio;
using Microsoft.EntityFrameworkCore;

namespace Cepedi.Banco.Analise.Dados.Repositories;
public class PessoaCreditoRepository : IPessoaCreditoRepository
{
    private readonly ApplicationDbContext _context;

    public PessoaCreditoRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<PessoaCreditoEntity> CriarPessoaCreditoAsync(PessoaCreditoEntity pessoaCredito)
    {
        _context.PessoaCredito.Add(pessoaCredito);

        return pessoaCredito;
    }
    public async Task<PessoaCreditoEntity> AtualizarPessoaCreditoAsync(PessoaCreditoEntity pessoaCredito)
    {
        _context.PessoaCredito.Update(pessoaCredito);

        return pessoaCredito;
    }


    public async Task<PessoaCreditoEntity> ObterPessoaCreditoAsync(string cpf)
    {
        return await _context.PessoaCredito.Where(e => e.Cpf == cpf).FirstOrDefaultAsync();
    }

    public async Task<PessoaCreditoEntity> DeletarPessoaCreditoAsync(string cpf)
    {
        var pessoaCredito = await _context.PessoaCredito.Where(e => e.Cpf == cpf).FirstOrDefaultAsync();

        _context.PessoaCredito.Remove(pessoaCredito);

        return pessoaCredito;
    }

}

