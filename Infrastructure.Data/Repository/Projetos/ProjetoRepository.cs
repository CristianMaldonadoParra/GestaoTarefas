using Domain.Common.Orm;
using Domain.Entities;
using Domain.Filter.Filters.Projetos;
using Domain.Interfaces.Repository;
using Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repository.Projetos
{
    public class ProjetoRepository : RepositoryBase, IProjetoRepository
    {
        private readonly DbContextProject _context;
        private readonly DbSet<Projeto> _dbSet;

        public ProjetoRepository(DbContextProject context)
        {
            _context = context;
            _dbSet = context.Set<Projeto>();
        }

        public Projeto Add(Projeto entity)
        {
            _dbSet.Add(entity);
            return entity;
        }

        public Projeto Update(Projeto entity)
        {
            _dbSet.Update(entity);
            return entity;
        }

        public IEnumerable<Projeto> Update(IEnumerable<Projeto> entities)
        {
            _dbSet.UpdateRange(entities);
            return entities;
        }

        public void Remove(Projeto entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<Projeto> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void RemoveRangeAndCommit(IEnumerable<Projeto> entities)
        {
            _dbSet.RemoveRange(entities);
            _context.SaveChanges();
        }

        public async Task<Projeto> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<(IEnumerable<Projeto> Result, int TotalCount)> GetWithFilters(ProjetosFilter filter)
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
