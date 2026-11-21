namespace EmailService.Contract;

public enum ContentType
{
    Text,
    HTML
}

public record EmailAddress(string Name, string Address);

public record Content(ContentType Type, string Value);

public record EmailMessage(string Subject, Content Content, EmailAddress From, List<EmailAddress> To);
