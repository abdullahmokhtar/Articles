using Blocks.Domain.Enities;
using Blocks.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Blocks.EntityFramework;

public static class RepositoryExtension
{
    public static async Task<TEntity> FindByIdOrThrowExceptionAsync<TEntity, TContext>(this Repository<TContext, TEntity> repository, int id, CancellationToken cancellationToken = default)
        where TEntity : class, IEntity
        where TContext : DbContext
    {
        var entity = await repository.FindByIdAsync(id, cancellationToken) ?? throw new NotFoundException($"{typeof(TEntity).Name} with Id {id} was not found.");
        return entity;
    }

    public static async Task<TEntity> FindByIdOrThrowExceptionAsync<TEntity>(this DbSet<TEntity> dbSet, int id, CancellationToken cancellationToken = default)
    where TEntity : class, IEntity
    {
        var entity = await dbSet.FindAsync(id, cancellationToken) ?? throw new NotFoundException($"{typeof(TEntity).Name} with Id {id} was not found.");
        return entity;
    }

    public static async Task<TEntity> GetByIdOrThrowAsync<TEntity, TContext>(this Repository<TContext, TEntity> repository, int id, CancellationToken cancellationToken = default)
    where TEntity : class, IEntity
    where TContext : DbContext
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken) ?? throw new NotFoundException($"{typeof(TEntity).Name} with Id {id} was not found.");
        return entity;
    }
}
