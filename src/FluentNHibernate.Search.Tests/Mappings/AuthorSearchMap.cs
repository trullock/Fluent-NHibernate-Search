using FluentNHibernate.Search.Mapping;
using FluentNHibernate.Search.Tests.Domain;

namespace FluentNHibernate.Search.Tests.Mappings
{
    public class AuthorSearchMap : DocumentMap<Author>
    {
        protected override void Configure()
        {
            Id(p => p.AuthorId).Bridge().Guid();
            Name("Author");

            Analyzer().Standard();

            Map(x => x.Name)
                .Analyzer().Simple()
                .Store().Yes()
                .Index().Tokenized();

            Embedded(x => x.Books).AsCollection();
        }
    }
}