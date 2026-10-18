using Blocks.Domain.Enities;
using Blocks.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Blocks.EntityFramework;

public static class RepositoryExtension
{
    public static Task<TEntity> FindByIdOrThrowExceptionAsync<TEntity, TContext>(this Repository<TContext, TEntity> repository, int id, CancellationToken cancellationToken = default)
        where TEntity : class, IEntity
        where TContext : DbContext
    {
        var entity = repository.FindByIdAsync(id, cancellationToken) ?? throw new NotFoundException($"{typeof(TEntity).Name} with Id {id} was not found.");
        return entity;
    }
}
