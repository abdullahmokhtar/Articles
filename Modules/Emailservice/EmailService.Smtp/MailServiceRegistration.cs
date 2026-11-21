using Blocks.Core;
using EmailService.Contract;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmailService.Smtp;

public static class MailServiceRegistration
{
    public static IServiceCollection AddSmtpEmailService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAndValidationOptions<EmailOptions>(configuration);
        services.AddSingleton<IEmailService, SmtpEmailService>();
        return services;
    }
}
