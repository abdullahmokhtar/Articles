using Blocks.Core.Cache;
using Blocks.Domain.Enities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Blocks.EntityFramework;

public class CashedRepository<TDbContext, TEntity, TId>(TDbContext dbContext, IMemoryCache cache)
    where TDbContext : DbContext
    where TEntity : class, IEntity<TId>, ICashable
    where TId : struct
{
    public IEnumerable<TEntity> GetAll()
        => cache.GetOrCreateByType(entry => dbContext.Set<TEntity>().AsNoTracking().ToList())!;

    public TEntity? GetById(TId id)
        => GetAll().Single(e => e.Id.Equals(id));
}
