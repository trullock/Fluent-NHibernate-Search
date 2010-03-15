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
            PostDelete<FullTextIndexEventListener>();
            PostUpdate<FullTextIndexEventListener>();
            PostInsert<FullTextIndexEventListener>();

            return this;
        }

        public ListenersPart PostInsert<TEventListener>() where TEventListener : IPostInsertEventListener, new()
        {
            return PostInsert(new TEventListener());
        }

        public ListenersPart PostInsert<TEventListener>(TEventListener listener)
            where TEventListener : IPostInsertEventListener
        {
            return SetListener(ListenerType.PostInsert, listener);
        }

        public ListenersPart PostUpdate<TEventListener>() where TEventListener : IPostUpdateEventListener, new()
        {
            return PostUpdate(new TEventListener());
        }

        public ListenersPart PostUpdate<TEventListener>(TEventListener listener)
            where TEventListener : IPostUpdateEventListener
        {
            return SetListener(ListenerType.PostUpdate, listener);
        }

        public ListenersPart PostDelete<TEventListener>() where TEventListener : IPostDeleteEventListener, new()
        {
            return PostDelete(new TEventListener());
        }

        public ListenersPart PostDelete<TEventListener>(TEventListener listener)
            where TEventListener : IPostDeleteEventListener
        {
            return SetListener(ListenerType.PostDelete, listener);
        }


        private ListenersPart SetListener(ListenerType type, object listener)
        {
            cfg.Configuration.SetListener(type, listener);
            return this;
        }
    }
}