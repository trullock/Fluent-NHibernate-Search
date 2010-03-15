using System;
using FluentNHibernate.Search.Mapping;
using FNHS.Samples.Domain;

namespace FNHS.Samples.SearchMappings
{
    public class AuthorSearchMap : DocumentMap<Author>
    {
        protected override void Configure()
        {
            Id(x => x.AuthorId).Bridge().Guid();
            Name("Author");

            Map(x => x.Name)
				.Index().Tokenized()
				.Store().Yes();
        }
    }
}