using FluentNHibernate.Search.Cfg.EventListeners;

namespace FluentNHibernate.Search.Cfg
{
	public static class ListenerConfiguration
	{
		public static CustomListenerConfiguration Custom
		{
			get { return new CustomListenerConfiguration(); }
		}

		public static DefaultListenerConfiguration Default
		{
			get { return new DefaultListenerConfiguration(); }
		}
	}
}