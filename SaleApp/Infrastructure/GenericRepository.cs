using Domain;
using Domain.DataExtensions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly myDBContext _dbContext;
        private readonly DbSet<T> _entitiySet;


        public GenericRepository(myDBContext dbContext)
        {
            _dbContext = dbContext;
            _entitiySet = _dbContext.Set<T>();
        }


        public void Add(T entity)
            => _dbContext.Add(entity);


        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
            => await _dbContext.AddAsync(entity);


        public void AddRange(IEnumerable<T> entities)
            => _dbContext.AddRange(entities);


        public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
            => await _dbContext.AddRangeAsync(entities);

        public int Count(Expression<Func<T, bool>> query)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync(Expression<Func<T, bool>> query)
        {
            throw new NotImplementedException();
        }

        public T Get(Expression<Func<T, bool>> expression)
            => _entitiySet.FirstOrDefault(expression);


        public IQueryable<T> GetAll()
            => _entitiySet.AsNoTracking();

        public IQueryable<T> GetAllAsync()
     => _entitiySet.AsQueryable();


        public IQueryable<T> GetAll(Expression<Func<T, bool>> expression)
            => _entitiySet.Where(expression).AsNoTracking();


        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
            => await _entitiySet.ToListAsync();


        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
            => await _entitiySet.Where(expression).ToListAsync(cancellationToken);


        public async Task<T> GetAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
            => await _entitiySet.FirstOrDefaultAsync(expression);

        public void Remove(T entity)
            => _dbContext.Remove(entity);


        public void RemoveRange(IEnumerable<T> entities)
            => _dbContext.RemoveRange(entities);


        public void Update(T entity)
            => _dbContext.Update(entity);


        public void UpdateRange(IEnumerable<T> entities)
            => _dbContext.UpdateRange(entities);


        public IQueryable<T> Paging(Expression<Func<T, bool>> query, int page, int size, out int rowsCount)
        {
            return Paging(query, page, size, null, isAscendingOrder: false, out rowsCount);
        }



        public async Task<(List<T> result, int rowsCount)> PagingAsync(Expression<Func<T, bool>> query, int page, int size)
        {
            return await PagingAsync(null, page, size, null, isAscendingOrder: false);
        }



        public IQueryable<T> Paging(int page, int size, out int rowsCount)
        {
            return Paging(null, page, size, null, isAscendingOrder: false, out rowsCount);
        }



        public async Task<(List<T> result, int rowsCount)> PagingAsync(int page, int size)
        {
            return await PagingAsync(null, page, size, null, isAscendingOrder: false);
        }



        public IQueryable<T> Paging(int page, int size, Expression<Func<T, object>> orderByProperty, bool isAscendingOrder, out int rowsCount)
        {
            IQueryable<T> query = GetAll();
            return query.Paging(page, size, orderByProperty, isAscendingOrder, out rowsCount);
        }



        public IQueryable<T> Paging(Expression<Func<T, bool>> query, int page, int size, Expression<Func<T, object>> orderByProperty, bool isAscendingOrder, out int rowsCount)
        {
            IQueryable<T> query2 = ((query == null) ? GetAll() : GetAll().Where(query));
            return query2.Paging(page, size, orderByProperty, isAscendingOrder, out rowsCount);
        }



        public async Task<(List<T> result, int rowsCount)> PagingAsync(Expression<Func<T, bool>> query, int page, int size, Expression<Func<T, object>> orderByProperty, bool isAscendingOrder)
        {
            IQueryable<T> q = ((query == null) ? GetAll() : GetAll().Where(query));
            return await q.PagingAsync(page, size, orderByProperty, isAscendingOrder);
        }
    }
}
