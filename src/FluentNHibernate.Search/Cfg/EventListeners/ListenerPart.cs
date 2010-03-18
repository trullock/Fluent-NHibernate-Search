using NHibernate.Event;

namespace FluentNHibernate.Search.Cfg.EventListeners
{
	public class ListenerPart
	{
		public ListenerType Type { get; private set; }
		public object Listener { get; private set; }

		public ListenerPart(ListenerType type, object listener)
		{
			this.Type = type;
			this.Listener = listener;
		}
	}
}