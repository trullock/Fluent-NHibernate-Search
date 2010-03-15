using NHibernate.Search.Mapping;

namespace FluentNHibernate.Search.Mapping.Providers
{
    public interface IDocumentIdMappingProvider : IMappingProvider
    {
        DocumentIdMapping GetMapping();
    }
}