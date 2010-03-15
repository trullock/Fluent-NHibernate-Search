using FluentNHibernate.Mapping;
using FNHS.Samples.Domain;

namespace FNHS.Samples.Mappings
{
    public class BookMap : ClassMap<Book>
    {
        public BookMap()
        {
            Table("Book");
            Id(x => x.BookId).GeneratedBy.Assigned();

            Map(x => x.Title);
            Map(x => x.Description);

            References(x => x.Author);
        }
    }
}