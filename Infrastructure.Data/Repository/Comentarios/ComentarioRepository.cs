using Domain.Common.Orm;
using Domain.Entities;
using Domain.Filter.Filters.Comentarios;
using Domain.Interfaces.Repository;
using Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repository.Comentarios
{
    public class ComentarioRepository : RepositoryBase, IComentarioRepository
    {
        private readonly DbContextProject _context;
        private readonly DbSet<Comentario> _dbSet;

        public ComentarioRepository(DbContextProject context)
        {
            _context = context;
            _dbSet = context.Set<Comentario>();
        }

        public Comentario Add(Comentario entity)
        {
            _dbSet.Add(entity);
            return entity;
        }

        public Comentario Update(Comentario entity)
        {
            _dbSet.Update(entity);
            return entity;
        }

        public IEnumerable<Comentario> Update(IEnumerable<Comentario> entities)
        {
            _dbSet.UpdateRange(entities);
            return entities;
        }

        public void Remove(Comentario entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<Comentario> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void RemoveRangeAndCommit(IEnumerable<Comentario> entities)
        {
            _dbSet.RemoveRange(entities);
            _context.SaveChanges();
        }

        public async Task<Comentario> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<(IEnumerable<Comentario> Result, int TotalCount)> GetWithFilters(ComentarioFilter filter)
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
