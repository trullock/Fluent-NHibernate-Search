using FluentNHibernate.Mapping;
using FluentNHibernate.Search.Tests.Domain;

namespace FluentNHibernate.Search.Tests.Mappings
{
    public class TestDocumentMap : ClassMap<TestDocument>
    {
        public TestDocumentMap()
        {
            Id(d => d.Id);
            Map(d => d.StringProperty);
            Not.LazyLoad();
        }
    }
}