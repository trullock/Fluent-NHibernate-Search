using NHibernate.Search.Attributes;

namespace FluentNHibernate.Search.Mapping.Parts
{
    public interface IHasIndex
    {
        Index? Index { get; set; }
    }
}