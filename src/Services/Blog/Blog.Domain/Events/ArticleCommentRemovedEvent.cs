namespace Blog.Domain.Events
{
    public class ArticleCommentRemovedEvent
    {
        public ArticleCommentRemovedEvent(string userId, int commentId)
        {
            UserId = userId;
            CommentId = commentId;
        }

        public string UserId { get; }
        public int CommentId { get; }
    }
}
