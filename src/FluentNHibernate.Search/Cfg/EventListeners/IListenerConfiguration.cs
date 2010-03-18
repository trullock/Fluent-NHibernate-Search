using NHibernate.Cfg;

namespace FluentNHibernate.Search.Cfg.EventListeners
{
	public interface IListenerConfiguration
	{
		void Apply(Configuration cfg);
	}
}