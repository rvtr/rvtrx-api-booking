using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RVTR.Booking.Domain.Interfaces
{
  public interface IRepository<TEntity> where TEntity : AEntity
  {
    /// <summary>
    ///
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteAsync(int id);

    /// <summary>
    ///
    /// </summary>
    /// <param name="entry"></param>
    /// <returns></returns>
    Task InsertAsync(TEntity entry);

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<TEntity>> SelectAsync();

    /// <summary>
    ///
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    Task<IEnumerable<TEntity>> SelectAsync(Expression<Func<TEntity, bool>> predicate);
    void Update(TEntity entry);
  }
}
