using NHibernate.Cfg;

namespace FluentNHibernate.Search.Cfg
{
    public interface IFluentSearchConfiguration
    {
    	Configuration Configuration { get; }
    	Configuration BuildConfiguration();
    }
}