using System;
using System.Collections.Generic;
using FNHS.Samples.Domain;
using NHibernate;

namespace FNHS.Samples
{
    class Program
    {
        private static SessionFactory SessionFactory;

		static void Main(string[] args)
		{
			SessionFactory = new SessionFactory();

            using (ISession session = SessionFactory.CreateSession())
            {
                using (ITransaction tx = session.BeginTransaction())
                {
                    var author = new Author
                                     {
                                         Name = "Some Author"
                                     };
                    var book = new Book(author)
                                   {
                                       Title = "My Book",
                                       Description = "My Book Description"
                                   };
                    author.Books.Add(book);

                    session.SaveOrUpdate(author);
                    tx.Commit();
                }

                IList<Author> authors = session.CreateCriteria(typeof (Author)).List<Author>();
                IList<Book> books = session.CreateCriteria(typeof (Book)).List<Book>();

                Console.WriteLine("Authors : " + authors.Count);
                Console.WriteLine("Books : " + books.Count);
            	
				Console.ReadLine();
            }
        }
    }
}