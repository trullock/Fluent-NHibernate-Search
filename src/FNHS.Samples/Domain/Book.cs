using System;

namespace FNHS.Samples.Domain
{
    public class Book
    {
        protected Book()
        {
        }

        public Book(Author author)
        {
            Author = author;
            BookId = Guid.NewGuid();
        }

        public virtual Guid BookId { get; protected set; }
        public virtual Author Author { get; protected set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
    }
}