using System;
using System.Collections.Generic;

namespace FNHS.Samples.Domain
{
    public class Author
    {
        public Author()
        {
            AuthorId = Guid.NewGuid();

            Books = new List<Book>();
        }

        public virtual Guid AuthorId { get; private set; }
        public virtual string Name { get; set; }
        public virtual IList<Book> Books { get; private set; }
    }
}