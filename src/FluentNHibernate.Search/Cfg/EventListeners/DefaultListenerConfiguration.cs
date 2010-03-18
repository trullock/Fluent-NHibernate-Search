using NHibernate.Cfg;
using NHibernate.Event;
using NHibernate.Search.Event;

namespace FluentNHibernate.Search.Cfg.EventListeners
{
	public class DefaultListenerConfiguration : IListenerConfiguration
	{
		internal DefaultListenerConfiguration()
		{
		}

		public void Apply(Configuration cfg)
		{
			cfg.SetListener(ListenerType.PostUpdate, new FullTextIndexEventListener());
			cfg.SetListener(ListenerType.PostInsert, new FullTextIndexEventListener());
			cfg.SetListener(ListenerType.PostDelete, new FullTextIndexEventListener());
		}
	}
}