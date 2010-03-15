using System;

namespace FluentNHibernate.Search.Tests.Domain
{
    public class Book
    {
        public Guid BookId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}