using FluentNHibernate.Mapping;
using FNHS.Samples.Domain;

namespace FNHS.Samples.Mappings
{
    public class AuthorMap : ClassMap<Author>
    {
        public AuthorMap()
        {
            Table("Author");
            Id(x => x.AuthorId).GeneratedBy.Assigned();

            Map(x => x.Name);

            HasMany(x => x.Books)
                .KeyColumn("AuthorId")
                .Cascade.All()
                .Inverse()
                .AsBag();
        }
    }
}