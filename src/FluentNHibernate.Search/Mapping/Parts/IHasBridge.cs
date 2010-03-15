using NHibernate.Search.Bridge;

namespace FluentNHibernate.Search.Mapping.Parts
{
    public interface IHasBridge
    {
        IFieldBridge FieldBridge { get; set; }
    }
}