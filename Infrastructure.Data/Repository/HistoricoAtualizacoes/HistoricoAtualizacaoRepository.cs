using Domain.Common.Orm;
using Domain.Entities;
using Domain.Filter.Filters.HistoricoAtualizacoes;
using Domain.Interfaces.Repository;
using Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repository.HistoricoAtualizacoes
{
    public class HistoricoAtualizacaoRepository : RepositoryBase, IHistoricoAtualizacaoRepository
    {
        private readonly DbContextProject _context;
        private readonly DbSet<HistoricoAtualizacao> _dbSet;

        public HistoricoAtualizacaoRepository(DbContextProject context)
        {
            _context = context;
            _dbSet = context.Set<HistoricoAtualizacao>();
        }

        public HistoricoAtualizacao Add(HistoricoAtualizacao entity)
        {
            _dbSet.Add(entity);
            return entity;
        }

        public HistoricoAtualizacao Update(HistoricoAtualizacao entity)
        {
            _dbSet.Update(entity);
            return entity;
        }

        public IEnumerable<HistoricoAtualizacao> Update(IEnumerable<HistoricoAtualizacao> entities)
        {
            _dbSet.UpdateRange(entities);
            return entities;
        }

        public void Remove(HistoricoAtualizacao entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<HistoricoAtualizacao> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void RemoveRangeAndCommit(IEnumerable<HistoricoAtualizacao> entities)
        {
            _dbSet.RemoveRange(entities);
            _context.SaveChanges();
        }

        public async Task<HistoricoAtualizacao> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<(IEnumerable<HistoricoAtualizacao> Result, int TotalCount)> GetWithFilters(HistoricoAtualizacaoFilter filter)
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
