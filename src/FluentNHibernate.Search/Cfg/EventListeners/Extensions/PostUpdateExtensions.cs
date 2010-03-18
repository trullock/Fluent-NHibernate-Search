using System;
using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.Search.Cfg.EventListeners;
using NHibernate.Event;

// Deliberately wrong to aid discoverability
namespace FluentNHibernate.Search.Cfg
{
	public static class PostUpdateExtensions
	{
		/// <summary>
		/// Sets an IPostUpdateEventListeners
		/// </summary>
		/// <typeparam name="TEventListener">The type of an event listener to register for the PostUpdate event</typeparam>
		/// <returns></returns>
		public static CustomListenerConfiguration PostUpdate<TEventListener>(this CustomListenerConfiguration self) where TEventListener : IPostUpdateEventListener, new()
		{
			return self.PostUpdate(new TEventListener());
		}

		/// <summary>
		/// Sets an IPostUpdateEventListeners
		/// </summary>
		/// <param name="self"></param>
		/// <param name="listener">An event listener to register for the PostUpdate event</param>
		/// <returns></returns>
		public static CustomListenerConfiguration PostUpdate(this CustomListenerConfiguration self, IPostUpdateEventListener listener)
		{
			if (listener == null)
				throw new ArgumentNullException("listener", "FluentNHibernate.Search, Listeners.PostUpdate: `Listener` cannot be null");

			return self.AddListeners(ListenerType.PostUpdate, new[] { listener });
		}

		/// <summary>
		/// Sets a collection of IPostUpdateEventListeners
		/// </summary>
		/// <param name="self"></param>
		/// <param name="listeners">Event listeners to register for the PostUpdate event</param>
		/// <returns></returns>
		public static CustomListenerConfiguration PostUpdate(this CustomListenerConfiguration self, IEnumerable<IPostUpdateEventListener> listeners)
		{
			if (listeners == null)
				throw new ArgumentNullException("listeners", "FluentNHibernate.Search, Listeners.PostUpdate: `Listeners` cannot be null");

			if (!listeners.Any())
				throw new ArgumentException("FluentNHibernate.Search, Listeners.PostUpdate: `Listeners` does not contain any elements, it must contain at least one", "listeners");

			return self.AddListeners(ListenerType.PostUpdate, listeners.ToArray());
		}
	}
}