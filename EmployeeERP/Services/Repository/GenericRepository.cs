using AutoMapper;
using EmployeeERP.Data;
using EmployeeERP.Models;
using EmployeeERP.Models.DbEntities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EmployeeERP.Services.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GenericRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> AddAsync(T entity)
        {
            if (entity != null)
            {
                await _context.Set<T>().AddAsync(entity);
                var affectedEntities = await _context.SaveChangesAsync();
                return affectedEntities > 0;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                var affectedEntities = await _context.SaveChangesAsync();
                return affectedEntities > 0;
            }
            return false;
        }

        public async Task<ICollection<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetByIdIncludingAsync(Guid id, Expression<Func<T, Object>> condition)
        {
            return await _context.Set<T>().Include(condition).FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            if (await IsValidEntity(entity))
            {
                _context.Set<T>().Update(entity);
                var affectedEntities = await _context.SaveChangesAsync();
                return affectedEntities > 0;
            }
            return false;
        }

        private async Task<bool> IsValidEntity(T entity)
        {
            if (entity == null || await _context.Set<T>().FindAsync(entity.Id) == null)
            {
                return false;
            }
            return true;
        }

        public async Task<ICollection<T>> GetByConditionAsync(Expression<Func<T, bool>> condition, Expression<Func<T, Object>> condition2)
        {
            return await _context.Set<T>().Where(condition).Include(condition2).ToListAsync();
        }
    }
}
