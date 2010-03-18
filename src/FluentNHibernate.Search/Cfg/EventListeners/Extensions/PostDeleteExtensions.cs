using System;
using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.Search.Cfg.EventListeners;
using NHibernate.Event;

// Deliberately wrong to aid discoverability
namespace FluentNHibernate.Search.Cfg
{
	public static class PostDeleteExtensions
	{
		/// <summary>
		/// Sets an IPostDeleteEventListeners
		/// </summary>
		/// <typeparam name="TEventListener">The type of an event listener to register for the PostDelete event</typeparam>
		/// <returns></returns>
		public static CustomListenerConfiguration PostDelete<TEventListener>(this CustomListenerConfiguration self) where TEventListener : IPostDeleteEventListener, new()
		{
			return self.PostDelete(new TEventListener());
		}

		/// <summary>
		/// Sets an IPostDeleteEventListeners
		/// </summary>
		/// <param name="self"></param>
		/// <param name="listener">An event listener to register for the PostDelete event</param>
		/// <returns></returns>
		public static CustomListenerConfiguration PostDelete(this CustomListenerConfiguration self, IPostDeleteEventListener listener)
		{
			if (listener == null)
				throw new ArgumentNullException("listener", "FluentNHibernate.Search, Listeners.PostDelete: `Listener` cannot be null");


			return self.AddListeners(ListenerType.PostDelete, new[] { listener });
		}

		/// <summary>
		/// Sets a collection of IPostDeleteEventListeners
		/// </summary>
		/// <param name="self"></param>
		/// <param name="listeners">Event listeners to register for the PostDelete event</param>
		/// <returns></returns>
		public static CustomListenerConfiguration PostDelete(this CustomListenerConfiguration self, IEnumerable<IPostDeleteEventListener> listeners)
		{
			if (listeners == null)
				throw new ArgumentNullException("listeners", "FluentNHibernate.Search, Listeners.PostDelete: `Listeners` cannot be null");

			if (!listeners.Any())
				throw new ArgumentException("FluentNHibernate.Search, Listeners.PostDelete: `Listeners` does not contain any elements, it must contain at least one", "listeners");

			return self.AddListeners(ListenerType.PostDelete, listeners.ToArray());
		}
	}
}