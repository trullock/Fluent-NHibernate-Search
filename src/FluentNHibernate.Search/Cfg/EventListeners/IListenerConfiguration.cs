using System.Collections.Generic;
using NHibernate.Cfg;

namespace FluentNHibernate.Search.Cfg.EventListeners
{
	public interface IListenerConfiguration
	{
		IList<ListenerPart> Parts { get; }
		void Apply(Configuration cfg);
	}
}