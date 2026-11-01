using Blocks.Core;
using Blocks.Domain.ValueObjects;
using System.Net.Mail;

namespace Submission.Domain.ValueObjects;

public class EmailAddress(string value) : StringValueObject(value)
{
    public static EmailAddress Create(string value)
    {
        Guard.ThrowIfNullOrWhitespace(value);
        if (!IsValidEmail(value))
            throw new ArgumentException("Invalid email address format.", nameof(value));
        return new EmailAddress(value);
    }

    private static bool IsValidEmail(string email)
    {
        try
        {
            var addr = new MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
}
