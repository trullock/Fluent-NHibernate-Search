using System;
using System.Collections.Generic;
using NHibernate.Cfg;
using NHibernate.Event;
using NHibernate.Search.Event;

namespace FluentNHibernate.Search.Cfg.EventListeners
{
	public class DefaultListenerConfiguration : IListenerConfiguration
	{
		public IList<ListenerPart> Parts
		{
			get { throw new NotSupportedException(); }
		}

		public void Apply(Configuration cfg)
		{
			cfg.SetListener(ListenerType.PostUpdate, new FullTextIndexEventListener());
			cfg.SetListener(ListenerType.PostInsert, new FullTextIndexEventListener());
			cfg.SetListener(ListenerType.PostDelete, new FullTextIndexEventListener());
		}
	}
}