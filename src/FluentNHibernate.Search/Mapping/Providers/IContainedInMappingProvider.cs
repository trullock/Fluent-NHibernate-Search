using NHibernate.Search.Mapping;

namespace FluentNHibernate.Search.Mapping.Providers
{
    public interface IContainedInMappingProvider : IMappingProvider
    {
        ContainedInMapping GetMapping();
    }
}