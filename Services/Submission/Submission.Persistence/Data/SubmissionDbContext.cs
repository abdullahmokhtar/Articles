using Microsoft.EntityFrameworkCore;
using Submission.Domain.Entities;

namespace Submission.Persistence.Data;

public class SubmissionDbContext : DbContext
{
    #region Entities
    public DbSet<Journal> Journals { get; set; }
    public DbSet<Article> Articles { get; set; }
    #endregion Entities

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SubmissionDbContext).Assembly);
    }
}
