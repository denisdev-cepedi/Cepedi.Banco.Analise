using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cepedi.Banco.Analise.Dominio.Repositorio;
public interface IUnitOfWork : IDisposable
{
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);

    IRepository<T> Repository<T>()
        where T : class;
}
