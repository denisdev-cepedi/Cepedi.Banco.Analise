using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cepedi.Banco.Analise.Dominio.Repositorio;
public interface IRepository<T> where T : class
{
    Task<T> GetByIdAsync(int id);
    Task<T> AddAsync(T entity, CancellationToken cancellationToken);
    T UpdateAsync(T entity);
    T DeleteAsync(T entity);
}
