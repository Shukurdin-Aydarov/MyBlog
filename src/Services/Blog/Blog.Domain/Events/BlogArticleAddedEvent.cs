namespace Blog.Domain.Events
{
    using MediatR;

    public class BlogArticleAddedEvent : INotification
    {
        public BlogArticleAddedEvent(int blogId, string title)
        {
            BlogId = blogId;
            Title = title;
        }

        public int BlogId { get; }
        public string Title { get; }
    }
}
