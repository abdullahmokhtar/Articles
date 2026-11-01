namespace Submission.Domain.Entities;

public partial class Journal : Entity
{
    public string Name { get; set; }
    public string Abreviation { get; set; }

    private readonly List<Article> _articles = new();
    public IReadOnlyList<Article> Articles => _articles.AsReadOnly();
}
