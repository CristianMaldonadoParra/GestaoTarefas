using Domain.Common.Orm;
using Domain.Entities;
using Domain.Filter.Filters.Prioridades;
using Domain.Interfaces.Repository;
using Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repository.Prioridades
{
    public class PrioridadeRepository : RepositoryBase, IPrioridadeRepository
    {
        private readonly DbContextProject _context;
        private readonly DbSet<Prioridade> _dbSet;

        public PrioridadeRepository(DbContextProject context)
        {
            _context = context;
            _dbSet = context.Set<Prioridade>();
        }

        public Prioridade Add(Prioridade entity)
        {
            _dbSet.Add(entity);
            return entity;
        }

        public Prioridade Update(Prioridade entity)
        {
            _dbSet.Update(entity);
            return entity;
        }

        public IEnumerable<Prioridade> Update(IEnumerable<Prioridade> entities)
        {
            _dbSet.UpdateRange(entities);
            return entities;
        }

        public void Remove(Prioridade entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<Prioridade> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void RemoveRangeAndCommit(IEnumerable<Prioridade> entities)
        {
            _dbSet.RemoveRange(entities);
            _context.SaveChanges();
        }

        public async Task<Prioridade> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<(IEnumerable<Prioridade> Result, int TotalCount)> GetWithFilters(PrioridadeFilter filter)
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
