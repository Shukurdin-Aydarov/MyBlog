namespace Blog.Domain.AggregatesModel.ArticleAggregate
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using global::Blog.Domain.Events;

    public class Article : Entity, IAggregateRoot
    {
        public Article(int blogId, string title, string text)
        {
            BlogId = blogId;
            Title = title;
            Text = text;
        }

        public int BlogId { get; }
        public string Title { get; }
        public string Text { get; }
                
        private readonly List<Tag> tags;
        public IReadOnlyCollection<Tag> Tags => tags.AsReadOnly();
    }
}
