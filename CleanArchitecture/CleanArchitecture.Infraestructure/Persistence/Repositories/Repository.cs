using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Infraestructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CleanArchitecture.Infraestructure.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : class,new()
    {
        private readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<T> Add(T entity)
        {
           await  _context.Set<T>().AddAsync(entity);
           await _context.SaveChangesAsync();
            return entity;
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }

        public async Task Delete(int id)
        {
            T entity = await GetById(id);
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task<T> FirstAsync(Expression<Func<T, bool>> predicate)
            => await _context.Set<T>().FirstOrDefaultAsync(predicate);

        public async Task<IEnumerable<T>> GetAll()
            => await _context.Set<T>().ToListAsync();

        public async Task<T> GetById(int id)
            => await _context.Set<T>().FindAsync(id);

        public async Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate)
            => await _context.Set<T>().Where(predicate).ToListAsync();

        public void UdateRange(IEnumerable<T> entities)
        {
            _context.Set<T>().UpdateRange(entities);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
