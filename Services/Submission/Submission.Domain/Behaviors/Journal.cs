using Articles.Abstractions.Enums;

namespace Submission.Domain.Entities;

public partial class Journal
{
    public Article CreateArticle(string title, ArticleType type, string scope)
    {
        var article = new Article
        {
            Title = title,
            Scope = scope,
            Type = type,
            Journal = this,
            Stage = ArticleStage.Created,
        };
        _articles.Add(article);
        //todo: add domain event
        return article;
    }
}
