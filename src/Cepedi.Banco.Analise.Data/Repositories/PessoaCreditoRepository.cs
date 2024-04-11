using Cepedi.Banco.Analise.Domain.Entities;
using Cepedi.Banco.Analise.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Cepedi.Banco.Analise.Data.Repositories;
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

        await _context.SaveChangesAsync();

        return pessoaCredito;
    }
    public async Task<PessoaCreditoEntity> AtualizarPessoaCreditoAsync(PessoaCreditoEntity pessoaCredito)
    {
        _context.PessoaCredito.Update(pessoaCredito);

        await _context.SaveChangesAsync();

        return pessoaCredito;
    }


    public async Task<PessoaCreditoEntity> ObterPessoaCreditoAsync(int id)
    {
        return await _context.PessoaCredito.Where(e => e.PessoaId == id).FirstOrDefaultAsync() ;
    }
}

