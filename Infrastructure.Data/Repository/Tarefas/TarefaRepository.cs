using Domain.Common.Orm;
using Domain.Entities;
using Domain.Filter.Filters.Tarefas;
using Domain.Interfaces.Repository;
using Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repository.Tarefas
{
    public class TarefaRepository : RepositoryBase, ITarefaRepository
    {
        private readonly DbContextProject _context;
        private readonly DbSet<Tarefa> _dbSet;

        public TarefaRepository(DbContextProject context)
        {
            _context = context;
            _dbSet = context.Set<Tarefa>();
        }

        public Tarefa Add(Tarefa entity)
        {
            _dbSet.Add(entity);
            return entity;
        }

        public Tarefa Update(Tarefa entity)
        {
            _dbSet.Update(entity);
            return entity;
        }

        public IEnumerable<Tarefa> Update(IEnumerable<Tarefa> entities)
        {
            _dbSet.UpdateRange(entities);
            return entities;
        }

        public void Remove(Tarefa entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<Tarefa> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void RemoveRangeAndCommit(IEnumerable<Tarefa> entities)
        {
            _dbSet.RemoveRange(entities);
            _context.SaveChanges();
        }

        public async Task<Tarefa> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<(IEnumerable<Tarefa> Result, int TotalCount)> GetWithFilters(TarefaFilter filter)
        {
            var query = _dbSet.WithFilters(filter)
                               .OrderByDomain(filter);

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
