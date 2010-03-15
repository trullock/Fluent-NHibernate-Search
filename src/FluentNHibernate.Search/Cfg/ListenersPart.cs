using NHibernate.Event;
using NHibernate.Search.Event;

namespace FluentNHibernate.Search.Cfg
{
	public class ListenersPart
	{
		private readonly IFluentSearchConfiguration cfg;

		public ListenersPart(IFluentSearchConfiguration cfg)
		{
			this.cfg = cfg;
		}

		public ListenersPart Default()
		{
			this.PostDelete<FullTextIndexEventListener>();
			this.PostUpdate<FullTextIndexEventListener>();
			this.PostInsert<FullTextIndexEventListener>();

			return this;
		}

		public ListenersPart PostInsert<TEventListener>() where TEventListener : IPostInsertEventListener, new()
		{
			return this.PostInsert(new TEventListener());
		}

		public ListenersPart PostInsert<TEventListener>(TEventListener listener) where TEventListener : IPostInsertEventListener
		{
			return this.setListener(ListenerType.PostInsert, listener);
		}

		public ListenersPart PostUpdate<TEventListener>() where TEventListener : IPostUpdateEventListener, new()
		{
			return this.PostUpdate(new TEventListener());
		}

		public ListenersPart PostUpdate<TEventListener>(TEventListener listener) where TEventListener : IPostUpdateEventListener
		{
			return this.setListener(ListenerType.PostUpdate, listener);
		}

		public ListenersPart PostDelete<TEventListener>() where TEventListener : IPostDeleteEventListener, new()
		{
			return this.PostDelete(new TEventListener());
		}

		public ListenersPart PostDelete<TEventListener>(TEventListener listener) where TEventListener : IPostDeleteEventListener
		{
			return this.setListener(ListenerType.PostDelete, listener);
		}


		private ListenersPart setListener(ListenerType type, object listener)
		{
			cfg.Configuration.SetListener(type, listener);
			return this;
		}
	}
}