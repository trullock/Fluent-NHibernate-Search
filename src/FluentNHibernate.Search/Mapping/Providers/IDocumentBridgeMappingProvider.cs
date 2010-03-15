using NHibernate.Search.Mapping;

namespace FluentNHibernate.Search.Mapping.Providers
{
    public interface IDocumentBridgeMappingProvider : IMappingProvider
    {
        ClassBridgeMapping GetMapping();
    }
}