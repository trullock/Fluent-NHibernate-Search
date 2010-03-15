using FluentNHibernate.Search.Mapping;
using FluentNHibernate.Search.Tests.Domain;

namespace FluentNHibernate.Search.Tests.Mappings
{
    public class BookSearchMap : DocumentMap<Book>
	{
        protected override void Configure()
        {
            Id(p => p.BookId).Bridge().Guid();
            Name("Book");

            Map(x => x.Title)
                .Store().Yes()
                .Index().Tokenized();
            Map(x => x.Description)
                .Store().Yes()
                .Index().Tokenized();
        }
    }
}