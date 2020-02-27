namespace Blog.Domain.Events
{
    using System;
    using MediatR;

    public class ArticleCommentAddedEvent : INotification
    {
        public ArticleCommentAddedEvent(string creatorId, string comment, DateTimeOffset creationDate)
        {
            CreatorId = creatorId;
            Comment = comment;
            CreationDate = creationDate;
        }

        public string CreatorId { get; }
        public DateTimeOffset CreationDate { get; }
        public string Comment { get; }
    }
}
