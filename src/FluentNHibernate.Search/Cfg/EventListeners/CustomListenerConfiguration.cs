using System;
using System.Collections.Generic;
using NHibernate.Cfg;

namespace FluentNHibernate.Search.Cfg.EventListeners
{
	public class CustomListenerConfiguration : IListenerConfiguration
	{
		protected IList<ListenerPart> parts;

		internal CustomListenerConfiguration()
		{
			this.parts = new List<ListenerPart>();
		}

		IList<ListenerPart> IListenerConfiguration.Parts
		{
			get { return this.parts; }
		}

		void IListenerConfiguration.Apply(Configuration cfg)
		{
			foreach (var part in this.parts)
				cfg.SetListener(part.Type, part.Listener);
		}
	}
}