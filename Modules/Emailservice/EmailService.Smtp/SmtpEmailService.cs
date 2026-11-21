using EmailService.Contract;
using MailKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;

namespace EmailService.Smtp;

public class SmtpEmailService : IEmailService
{
    private readonly EmailOptions _emailOptions;

    public SmtpEmailService(IOptions<EmailOptions> options)
    {
        _emailOptions = options.Value;
    }
    public async Task<bool> SendEmailAsync(EmailMessage emailMessage, CancellationToken ct = default)
    {
        var message = emailMessage.ToMailKitMesssage();

        using var smtpClient = new SmtpClient();

        try
        {
            await smtpClient.ConnectAsync(_emailOptions.Smtp.Host, _emailOptions.Smtp.Port, _emailOptions.Smtp.UseSSL, ct);
            await smtpClient.AuthenticateAsync(_emailOptions.Smtp.UserName, _emailOptions.Smtp.Password, ct);
            await smtpClient.SendAsync(message, ct);
        }
        catch
        {
            return false;
        }
        return true;
    }
}
