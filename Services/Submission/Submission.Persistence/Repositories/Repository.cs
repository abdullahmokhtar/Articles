using Blocks.Domain.Enities;
using Blocks.EntityFramework;
using Submission.Persistence.Data;

namespace Submission.Persistence.Repositories;

public class Repository<TEntity>(SubmissionDbContext dbContext) : Repository<SubmissionDbContext, TEntity>(dbContext) where TEntity : class, IEntity
{
}
