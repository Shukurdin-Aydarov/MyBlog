namespace Blog.Domain.AggregatesModel
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using global::Blog.Domain.AggregatesModel.ArticleAggregate;
    using global::Blog.Domain.Events;

    public class Blog : Entity, IAggregateRoot
    {
        public Blog(string ownerIdentity)
        {
            OwnerIdentity = !string.IsNullOrEmpty(ownerIdentity)
                ? ownerIdentity
                : throw new ArgumentNullException(nameof(ownerIdentity));
        }

        public string OwnerIdentity { get; }

        private readonly List<Article> articles;
        public IReadOnlyCollection<Article> Articles => articles.AsReadOnly();

        private readonly List<Comment> comments;

        public void AddArticle(string title, string text)
        {
            if (string.IsNullOrEmpty(title))
                throw new DomainException("An article title cannot be null or empty.");

            if (string.IsNullOrEmpty(text))
                throw new DomainException("An article body cannot be null or empty.");

            var article = new Article(Id, title, text);
            articles.Add(article);

            AddEvent(new BlogArticleAddedEvent(Id, title));
        }

        public void AddComment(int articleId, string text, string userId)
        {
            if (string.IsNullOrEmpty(userId))
                throw new DomainException("Cannot add comment without user identifier.");

            if (string.IsNullOrEmpty(text))
                throw new DomainException("Cannot add comment without text.");

            var article = articles.SingleOrDefault(a => a.Id == articleId);

            if (article == null)
                throw new DomainException($"Article with id '{articleId}' not found.");

            var comment = new Comment(userId, article.Id, text, DateTimeOffset.UtcNow);
            comments.Add(comment);

            AddEvent(new ArticleCommentAddedEvent(comment.UserId, comment.Text, comment.CreationDate));
        }

        public void RemoveComment(int commentId, string userId)
        {
            //todo
            var comment = comments.SingleOrDefault(x => x.Id == commentId);

            if (comment != null && hasAccess(userId))
            {
              comments.Remove(comment);
            }

            bool hasAccess(string u)
            {
                return u == comment.UserId || u == OwnerIdentity;
            }
        }
    }
}
