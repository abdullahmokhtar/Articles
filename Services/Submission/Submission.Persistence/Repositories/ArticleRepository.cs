using Submission.Domain.Entities;
using Submission.Persistence.Data;

namespace Submission.Persistence.Repositories;

public class ArticleRepository(SubmissionDbContext dbContext) : Repository<Article>(dbContext) { }
