using NHibernate.Search.Mapping;

namespace FluentNHibernate.Search.Mapping.Providers
{
    public interface IFieldMappingProvider : IMappingProvider
    {
        FieldMapping GetMapping();   
    }
}