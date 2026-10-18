using Blocks.Domain.Enities;
using Microsoft.EntityFrameworkCore;

namespace Blocks.EntityFramework;

public interface IRepository<TEntity> where TEntity : IEntity
{
    public Task<TEntity?> FindByIdAsync(int id);
    public Task<TEntity?> GetByIdAsync(int id);
    public Task<TEntity> AddAsync(TEntity entity);
    public TEntity Update(TEntity entity);
    public void Remove(TEntity entity);
    public Task<bool> DeleteByIdAsync(int id);
}

public class Repository<TContext, TEntity>
    where TEntity : class, IEntity
    where TContext : DbContext
{
    private readonly TContext _dbContext;
    private readonly DbSet<TEntity> _entity;

    public Repository(TContext dbContext)
    {
        _dbContext = dbContext;
        _entity = _dbContext.Set<TEntity>();
    }
    public TContext Context => _dbContext;
    public DbSet<TEntity> Entity => _entity;
    public string TableName => _dbContext.Model.FindEntityType(typeof(TEntity))?.GetTableName()!;

    protected virtual IQueryable<TEntity> Query() => _entity;

    public virtual async Task<TEntity?> FindByIdAsync(int id, CancellationToken ct = default) => await Query().FirstOrDefaultAsync(x => x.Id == id, ct);

    public virtual async Task<TEntity?> GetByIdAsync(int id) => await Query().FirstOrDefaultAsync(x => x.Id == id);

    public virtual async Task<TEntity> AddAsync(TEntity entity) => (await _entity.AddAsync(entity)).Entity;

    public virtual TEntity Update(TEntity entity) => _entity.Update(entity).Entity;

    public virtual void Remove(TEntity entity) => _entity.Remove(entity);

    public virtual async Task<bool> DeleteByIdAsync(int id)
    {
        var rowAffected = await _dbContext.Database.ExecuteSqlInterpolatedAsync($"DELETE FROM {TableName} WHERE Id = {id}");
        return rowAffected > 0;
    }

    public async Task<int> SaveChangesAsync(CancellationToken ct = default) => await _dbContext.SaveChangesAsync(ct);
}
