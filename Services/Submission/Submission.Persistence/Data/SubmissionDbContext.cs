using Microsoft.EntityFrameworkCore;
using Submission.Domain.Entities;

namespace Submission.Persistence.Data;

public class SubmissionDbContext(DbContextOptions<SubmissionDbContext> options) : DbContext(options)
{
    #region Entities
    public DbSet<Journal> Journals { get; set; }
    public DbSet<Article> Articles { get; set; }

    public DbSet<Person> Persons { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<ArticleActor> ArticleActors { get; set; }

    public DbSet<Asset> Assets { get; set; }
    public DbSet<AssetTypeDefination> AssetTypes { get; set; }
    #endregion Entities

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SubmissionDbContext).Assembly);
    }
}
