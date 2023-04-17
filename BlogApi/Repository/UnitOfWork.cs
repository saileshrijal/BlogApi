using BlogApi.Data;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;

namespace Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync<T>(T entity) where T : class
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public async Task CreateRangeAsync<T>(List<T> entities) where T : class
        {
            await _context.Set<T>().AddRangeAsync(entities);
        }

        public async Task DeleteAsync<T>(T entity) where T : class
        {
            _context.Set<T>().Remove(entity);
            await Task.CompletedTask;
        }

        public async Task DeleteRangeAsync<T>(List<T> entities) where T : class
        {
            _context.Set<T>().RemoveRange(entities);
            await Task.CompletedTask;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync<T>(T entity) where T : class
        {
            _context.Set<T>().Update(entity);
            await Task.CompletedTask;
        }

        public async Task UpdateRangeAsync<T>(List<T> entities) where T : class
        {
            _context.Set<T>().UpdateRange(entities);
            await Task.CompletedTask;
        }
    }
}