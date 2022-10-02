using System.Linq.Expressions;

namespace CleanArchitecture.Application.Common.Interfaces
{
    public interface IRepository<T> where T : class, new()
    {
        Task<T> GetById(int id);
        Task<T> FirstAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate);
        Task Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Update(T entity);
        void UdateRange(IEnumerable<T> entities);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
    }
}
