using System.Linq.Expressions;

namespace Domain
{
    public interface IGenericRepository<T> where T : class
    {
        T Get(Expression<Func<T, bool>> expression);
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(Expression<Func<T, bool>> expression);
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);
        Task<T> GetAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
        Task AddAsync(T entity, CancellationToken cancellationToken = default);
        Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
        int Count(Expression<Func<T, bool>> query);
        Task<int> CountAsync(Expression<Func<T, bool>> query);
        IQueryable<T> Paging(Expression<Func<T, bool>> query, int page, int size, out int rowsCount);
        Task<(List<T> result, int rowsCount)> PagingAsync(Expression<Func<T, bool>> query, int page, int size);
        IQueryable<T> Paging(int page, int size, out int rowsCount);
        Task<(List<T> result, int rowsCount)> PagingAsync(int page, int size);
        IQueryable<T> Paging(int page, int size, Expression<Func<T, object>> orderByProperty, bool isAscendingOrder, out int rowsCount);
    }
}
