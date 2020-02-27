namespace Blog.Domain.AggregatesModel.ArticleAggregate
{
    using System;

    public class Comment : Entity
    {
        public Comment(string userId, int articleId, string text, DateTimeOffset creationDate)
        {
            UserId = userId;
            ArticleId = articleId;
            Text = text;
            CreationDate = creationDate;
        }

        public int ArticleId { get; }

        public string UserId { get; }

        public DateTimeOffset CreationDate { get; }

        public string Text { get; }
    }
}
