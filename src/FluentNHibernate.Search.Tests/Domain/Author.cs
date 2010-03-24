using System;
using System.Collections.Generic;

namespace FluentNHibernate.Search.Tests.Domain
{
    public class Author
    {
        public Author()
        {
            this.Books = new List<Book>();
			this.Address = new Address();
        }

        public Guid AuthorId { get; set; }
        public string Name { get; set; }
    	public Address Address { get; set; }
        public IList<Book> Books { get; set; }
    }
}