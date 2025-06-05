using Domain.Common.Filters;
using Domain.Common.Model;

namespace Domain.Common.Interfaces
{
    public interface IRepository<T, TFilter>
    {
        Task<T> GetById(int id);
        Task<(IEnumerable<T> Result, int TotalCount)> GetWithFilters(TFilter filter);
        T Add(T entity);
        T Update(T entity);
        IEnumerable<T> Update(IEnumerable<T> entitys);
        void Remove(T entity);
        void RemoveRangeAndCommit(IEnumerable<T> entitys);
        void RemoveRange(IEnumerable<T> entitys);


        Task<int> CountAsync<T2>(IQueryable<T2> source);
        Task<List<T2>> ToListAsync<T2>(IQueryable<T2> source);
        Task<int> CommitAsync();
    }
}
