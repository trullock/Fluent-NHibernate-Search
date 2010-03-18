using System;
using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.Search.Cfg.EventListeners;
using NHibernate.Event;

// Deliberately wrong to aid discoverability
namespace FluentNHibernate.Search.Cfg
{
	public static class PostInsertExtensions
	{
		/// <summary>
		/// Sets an IPostInsertEventListeners
		/// </summary>
		/// <typeparam name="TEventListener">The type of an event listener to register for the PostInsert event</typeparam>
		/// <returns></returns>
		public static CustomListenerConfiguration PostInsert<TEventListener>(this CustomListenerConfiguration self) where TEventListener : IPostInsertEventListener, new()
		{
			return self.PostInsert(new TEventListener());
		}

		/// <summary>
		/// Sets an IPostInsertEventListeners
		/// </summary>
		/// <param name="self"></param>
		/// <param name="listener">An event listener to register for the PostInsert event</param>
		/// <returns></returns>
		public static CustomListenerConfiguration PostInsert(this CustomListenerConfiguration self, IPostInsertEventListener listener)
		{
			if (listener == null)
				throw new ArgumentNullException("listener", "FluentNHibernate.Search, Listeners.PostInsert: `Listener` cannot be null");


			return self.AddListeners(ListenerType.PostInsert, new[] { listener });
		}

		/// <summary>
		/// Sets a collection of IPostInsertEventListeners
		/// </summary>
		/// <param name="self"></param>
		/// <param name="listeners">Event listeners to register for the PostInsert event</param>
		/// <returns></returns>
		public static CustomListenerConfiguration PostInsert(this CustomListenerConfiguration self, IEnumerable<IPostInsertEventListener> listeners)
		{
			if (listeners == null)
				throw new ArgumentNullException("listeners", "FluentNHibernate.Search, Listeners.PostInsert: `Listeners` cannot be null");

			if (!listeners.Any())
				throw new ArgumentException("FluentNHibernate.Search, Listeners.PostInsert: `Listeners` does not contain any elements, it must contain at least one", "listeners");

			return self.AddListeners(ListenerType.PostInsert, listeners.ToArray());
		}
	}
}