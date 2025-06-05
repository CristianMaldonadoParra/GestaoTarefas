using Domain.Common.Orm;
using Domain.Entities;
using Domain.Filter.Filters.StatusTarefa;
using Domain.Interfaces.Repository;
using Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repository.StatusTarefas
{
    public class StatusTarefaRepository : RepositoryBase, IStatusTarefaRepository
    {
        private readonly DbContextProject _context;
        private readonly DbSet<StatusTarefa> _dbSet;

        public StatusTarefaRepository(DbContextProject context)
        {
            _context = context;
            _dbSet = context.Set<StatusTarefa>();
        }

        public StatusTarefa Add(StatusTarefa entity)
        {
            _dbSet.Add(entity);
            return entity;
        }

        public StatusTarefa Update(StatusTarefa entity)
        {
            _dbSet.Update(entity);
            return entity;
        }

        public IEnumerable<StatusTarefa> Update(IEnumerable<StatusTarefa> entities)
        {
            _dbSet.UpdateRange(entities);
            return entities;
        }

        public void Remove(StatusTarefa entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<StatusTarefa> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void RemoveRangeAndCommit(IEnumerable<StatusTarefa> entities)
        {
            _dbSet.RemoveRange(entities);
            _context.SaveChanges();
        }

        public async Task<StatusTarefa> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<(IEnumerable<StatusTarefa> Result, int TotalCount)> GetWithFilters(StatusTarefaFilter filter)
        {
            var query = _dbSet.WithFilters(filter)
                               .OrderByDomain(filter)
                               .OrderByProperty(filter);

            var totalCount = await CountAsync(query);
            var paginateResult = Paging(query, filter, totalCount);

            return (paginateResult, totalCount);
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
