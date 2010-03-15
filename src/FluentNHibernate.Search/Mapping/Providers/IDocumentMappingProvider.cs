using NHibernate.Search.Mapping;

namespace FluentNHibernate.Search.Mapping.Providers
{
    public interface IDocumentMappingProvider : IMappingProvider
    {
        DocumentMapping GetMapping();
    }
}