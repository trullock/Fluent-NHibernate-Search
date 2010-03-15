using FluentNHibernate.Search.Cfg.EventListeners;
using NHibernate.Event;

// Deliberately incorrect to aid discoverability
namespace FluentNHibernate.Search.Cfg
{
	public static class CustomListenerConfigurationExtensions
	{
		public static CustomListenerConfiguration PostInsert<TEventListener>(this CustomListenerConfiguration self) where TEventListener : IPostInsertEventListener, new()
		{
			return self.PostInsert(new TEventListener());
		}

		public static CustomListenerConfiguration PostInsert<TEventListener>(this CustomListenerConfiguration self, TEventListener listener) where TEventListener : IPostInsertEventListener
		{
			return self.setListener(ListenerType.PostInsert, listener);
		}

		public static CustomListenerConfiguration PostUpdate<TEventListener>(this CustomListenerConfiguration self) where TEventListener : IPostUpdateEventListener, new()
		{
			return self.PostUpdate(new TEventListener());
		}

		public static CustomListenerConfiguration PostUpdate<TEventListener>(this CustomListenerConfiguration self, TEventListener listener) where TEventListener : IPostUpdateEventListener
		{
			return self.setListener(ListenerType.PostUpdate, listener);
		}

		public static CustomListenerConfiguration PostDelete<TEventListener>(this CustomListenerConfiguration self) where TEventListener : IPostDeleteEventListener, new()
		{
			return self.PostDelete(new TEventListener());
		}

		public static CustomListenerConfiguration PostDelete<TEventListener>(this CustomListenerConfiguration self, TEventListener listener) where TEventListener : IPostDeleteEventListener
		{
			return self.setListener(ListenerType.PostDelete, listener);
		}

		private static CustomListenerConfiguration setListener(this CustomListenerConfiguration self, ListenerType type, object listener)
		{
			(self as IListenerConfiguration).Parts.Add(new ListenerPart(type, listener));
			return self;
		}
	}
}