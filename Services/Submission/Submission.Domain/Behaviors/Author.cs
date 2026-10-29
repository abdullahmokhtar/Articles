using Submission.Domain.ValueObjects;

namespace Submission.Domain.Entities;


public partial class Author : Person
{
    private Author()
    {

    }

    public static Author Create(string email, string firstName, string lastName, string title, string affiliation)
    {
        return new Author
        {
            EmailAddress = EmailAddress.Create(email),
            FirstName = firstName,
            LastName = lastName,
            Title = title,
            Affiliation = affiliation
        };
    }


}
