using Cepedi.Banco.Analise.Dominio.Repositorio;

namespace Cepedi.Banco.Analise.Dados.Repositories;
public class Repository<T> :
    IDisposable, IRepository<T> where T : class
{
    private readonly ApplicationDbContext _context;
    private bool dispose;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<T> GetByIdAsync(int id)
    {

        return await _context.Set<T>().FindAsync(id);
    }


    public async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
    {
        await _context.Set<T>().AddAsync(entity, cancellationToken);

        return entity;
    }

    public T UpdateAsync(T entity)
    {
        _context.Set<T>().Update(entity);
        return entity;
    }

    public T DeleteAsync(T entity)
    {
        _context.Set<T>().Remove(entity);
        return entity;
    }

    private void Dispose(bool disposing)
    {
        if (!dispose && disposing)
        {
            _context?.Dispose();
        }

        dispose = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}

