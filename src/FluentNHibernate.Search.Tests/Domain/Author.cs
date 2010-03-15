using System;
using System.Collections.Generic;

namespace FluentNHibernate.Search.Tests.Domain
{
    public class Author
    {
        public Author()
        {
            Books = new List<Book>();
        }

        public Guid AuthorId { get; set; }
        public string Name { get; set; }
        public IList<Book> Books { get; set; }
    }
}