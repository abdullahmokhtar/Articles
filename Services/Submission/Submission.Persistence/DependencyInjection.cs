using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Submission.Persistence.Data;
using Submission.Persistence.Repositories;

namespace Submission.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<SubmissionDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
        services.AddScoped(typeof(Repository<>));
        services.AddScoped(typeof(ArticleRepository));
        return services;
    }
}
