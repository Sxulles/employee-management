using EmployeeERP.Models;
using System.Linq.Expressions;

namespace EmployeeERP.Services.Repository
{
    public interface IGenericRepository<T> where T : class, IEntity
    {
        Task<ICollection<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task<T> GetByIdIncludingAsync(Guid id, Expression<Func<T, Object>> condition);
        Task<bool> AddAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(Guid entity);
        Task<ICollection<T>> GetByConditionAsync(Expression<Func<T, bool>> condition, Expression<Func<T, Object>> condition2);
    }
}
