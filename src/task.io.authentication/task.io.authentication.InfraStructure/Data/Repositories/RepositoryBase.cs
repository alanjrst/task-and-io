using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using task.io.authentication.Domain.Base;
using task.io.authentication.Domain.Entities.Interfaces;

namespace task.io.authentication.InfraStructure.Data
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : BaseEntity
    {
        private readonly DbSet<TEntity> _dbSet;
        private readonly EFContext _dbContext;
        protected IQueryable<TEntity> Query { get; private set; }

        public RepositoryBase(EFContext dbContext)
        {
            _dbSet = dbContext.Set<TEntity>();
            _dbContext = dbContext;
            Query = _dbSet.AsQueryable();
        }

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Create(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task ExecuteCommand(string command)
        {
            await _dbContext.Database.ExecuteSqlRawAsync(command);
        }

        public async Task<IList<TEntity>> GetAll()
        {
            return await Query.ToListAsync();
        }

        public async Task<TEntity> GetById(long id)
        {
            return await Query.FirstOrDefaultAsync(d => d.Id == id);
        }

        public IQueryable<TEntity> GetQueryable()
        {
            return _dbContext.Set<TEntity>().AsNoTracking();
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }
    }
}