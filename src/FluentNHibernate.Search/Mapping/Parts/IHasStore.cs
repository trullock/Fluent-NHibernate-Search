using NHibernate.Search.Attributes;

namespace FluentNHibernate.Search.Mapping.Parts
{
    public interface IHasStore
    {
        Store? Store { get; set; }
    }
}