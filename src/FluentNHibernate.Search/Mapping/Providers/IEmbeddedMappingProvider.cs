using NHibernate.Search.Mapping;

namespace FluentNHibernate.Search.Mapping.Providers
{
    public interface IEmbeddedMappingProvider : IMappingProvider
    {
        EmbeddedMapping GetMapping();
    }
}