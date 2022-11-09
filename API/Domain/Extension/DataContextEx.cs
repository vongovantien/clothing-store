using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Domain.DataExtensions
{
    public static class DataContextEx
    {
        public static IQueryable<T> Paging<T, TResult>(this IQueryable<T> query, int page, int size, Expression<Func<T, TResult>> orderByProperty, bool isAscendingOrder, out int rowsCount)
        {
            rowsCount = query.Count();
            if (rowsCount <= size || page < 1)
            {
                page = 1;
            }
            if (orderByProperty != null)
            {
                query = (isAscendingOrder ? query.OrderBy(orderByProperty) : query.OrderByDescending(orderByProperty));
            }
            int count = (page - 1) * size;
            return query.Skip(count).Take(size);
        }

        public static async Task<(List<T> result, int rowsCount)> PagingAsync<T, TResult>(this IQueryable<T> query, int page, int size, Expression<Func<T, TResult>> orderByProperty, bool isAscendingOrder)
        {
            int count = query.Count();
            if (count <= size || page < 1)
            {
                page = 1;
            }

            if (orderByProperty != null)
            {
                query = (isAscendingOrder ? query.OrderBy(orderByProperty) : query.OrderByDescending(orderByProperty));
            }

            int excludedRows = (page - 1) * size;
            return (await query.Skip(excludedRows).Take(size).ToListAsync(), count);
        }
    }
}
