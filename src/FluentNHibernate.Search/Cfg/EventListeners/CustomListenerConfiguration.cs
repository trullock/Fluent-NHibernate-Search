using System.Collections.Generic;
using System.Linq;
using NHibernate.Cfg;
using NHibernate.Event;

namespace FluentNHibernate.Search.Cfg.EventListeners
{
	public class CustomListenerConfiguration : IListenerConfiguration
	{
		protected IDictionary<ListenerType, IList<object>> listeners;

		internal CustomListenerConfiguration()
		{
			this.listeners = new Dictionary<ListenerType, IList<object>>();
		}

		/// <summary>
		/// Here be dragons. Damn NHibernate design.
		/// TODO: refactor this monstrocity
		/// </summary>
		/// <param name="cfg"></param>
		void IListenerConfiguration.Apply(Configuration cfg)
		{
			foreach (var listenerType in listeners.Keys)
			{
				switch (listenerType)
				{
					case ListenerType.Autoflush:
						cfg.EventListeners.AutoFlushEventListeners = this.listeners[listenerType].Cast<IAutoFlushEventListener>().ToArray();
						break;

					case ListenerType.Merge:
						cfg.EventListeners.MergeEventListeners = this.listeners[listenerType].Cast<IMergeEventListener>().ToArray();
						break;

					case ListenerType.Create:
						cfg.EventListeners.PersistEventListeners = this.listeners[listenerType].Cast<IPersistEventListener>().ToArray();
						break;

					case ListenerType.CreateOnFlush:
						cfg.EventListeners.PersistOnFlushEventListeners = this.listeners[listenerType].Cast<IPersistEventListener>().ToArray();
						break;

					case ListenerType.Delete:
						cfg.EventListeners.DeleteEventListeners = this.listeners[listenerType].Cast<IDeleteEventListener>().ToArray();
						break;

					case ListenerType.DirtyCheck:
						cfg.EventListeners.DirtyCheckEventListeners = this.listeners[listenerType].Cast<IDirtyCheckEventListener>().ToArray();
						break;

					case ListenerType.Evict:
						cfg.EventListeners.EvictEventListeners = this.listeners[listenerType].Cast<IEvictEventListener>().ToArray();
						break;

					case ListenerType.Flush:
						cfg.EventListeners.FlushEventListeners = this.listeners[listenerType].Cast<IFlushEventListener>().ToArray();
						break;

					case ListenerType.FlushEntity:
						cfg.EventListeners.FlushEntityEventListeners = this.listeners[listenerType].Cast<IFlushEntityEventListener>().ToArray();
						break;

					case ListenerType.Load:
						cfg.EventListeners.LoadEventListeners = this.listeners[listenerType].Cast<ILoadEventListener>().ToArray();
						break;

					case ListenerType.LoadCollection:
						cfg.EventListeners.InitializeCollectionEventListeners = this.listeners[listenerType].Cast<IInitializeCollectionEventListener>().ToArray();
						break;

					case ListenerType.Lock:
						cfg.EventListeners.LockEventListeners = this.listeners[listenerType].Cast<ILockEventListener>().ToArray();
						break;

					case ListenerType.Refresh:
						cfg.EventListeners.RefreshEventListeners = this.listeners[listenerType].Cast<IRefreshEventListener>().ToArray();
						break;

					case ListenerType.Replicate:
						cfg.EventListeners.ReplicateEventListeners = this.listeners[listenerType].Cast<IReplicateEventListener>().ToArray();
						break;

					case ListenerType.SaveUpdate:
						cfg.EventListeners.SaveOrUpdateEventListeners = this.listeners[listenerType].Cast<ISaveOrUpdateEventListener>().ToArray();
						break;

					case ListenerType.Save:
						cfg.EventListeners.SaveEventListeners = this.listeners[listenerType].Cast<ISaveOrUpdateEventListener>().ToArray();
						break;

					case ListenerType.PreUpdate:
						cfg.EventListeners.PreUpdateEventListeners = this.listeners[listenerType].Cast<IPreUpdateEventListener>().ToArray();
						break;

					case ListenerType.Update:
						cfg.EventListeners.UpdateEventListeners = this.listeners[listenerType].Cast<ISaveOrUpdateEventListener>().ToArray();
						break;

					case ListenerType.PreLoad:
						cfg.EventListeners.PreLoadEventListeners = this.listeners[listenerType].Cast<IPreLoadEventListener>().ToArray();
						break;

					case ListenerType.PreDelete:
						cfg.EventListeners.PreDeleteEventListeners = this.listeners[listenerType].Cast<IPreDeleteEventListener>().ToArray();
						break;

					case ListenerType.PreInsert:
						cfg.EventListeners.PreInsertEventListeners = this.listeners[listenerType].Cast<IPreInsertEventListener>().ToArray();
						break;

					case ListenerType.PreCollectionRecreate:
						cfg.EventListeners.PreCollectionRecreateEventListeners = this.listeners[listenerType].Cast<IPreCollectionRecreateEventListener>().ToArray();
						break;

					case ListenerType.PreCollectionRemove:
						cfg.EventListeners.PreCollectionRemoveEventListeners = this.listeners[listenerType].Cast<IPreCollectionRemoveEventListener>().ToArray();
						break;

					case ListenerType.PreCollectionUpdate:
						cfg.EventListeners.PreCollectionUpdateEventListeners = this.listeners[listenerType].Cast<IPreCollectionUpdateEventListener>().ToArray();
						break;

					case ListenerType.PostLoad:
						cfg.EventListeners.PostLoadEventListeners = this.listeners[listenerType].Cast<IPostLoadEventListener>().ToArray();
						break;

					case ListenerType.PostInsert:
						cfg.EventListeners.PostInsertEventListeners = this.listeners[listenerType].Cast<IPostInsertEventListener>().ToArray();
						break;

					case ListenerType.PostUpdate:
						cfg.EventListeners.PostUpdateEventListeners = this.listeners[listenerType].Cast<IPostUpdateEventListener>().ToArray();
						break;

					case ListenerType.PostDelete:
						cfg.EventListeners.PostDeleteEventListeners = this.listeners[listenerType].Cast<IPostDeleteEventListener>().ToArray();
						break;

					case ListenerType.PostCommitUpdate:
						cfg.EventListeners.PostCommitUpdateEventListeners = this.listeners[listenerType].Cast<IPostUpdateEventListener>().ToArray();
						break;

					case ListenerType.PostCommitInsert:
						cfg.EventListeners.PostCommitInsertEventListeners = this.listeners[listenerType].Cast<IPostInsertEventListener>().ToArray();
						break;

					case ListenerType.PostCommitDelete:
						cfg.EventListeners.PostCommitDeleteEventListeners = this.listeners[listenerType].Cast<IPostDeleteEventListener>().ToArray();
						break;

					case ListenerType.PostCollectionRecreate:
						cfg.EventListeners.PostCollectionRecreateEventListeners = this.listeners[listenerType].Cast<IPostCollectionRecreateEventListener>().ToArray();
						break;

					case ListenerType.PostCollectionRemove:
						cfg.EventListeners.PostCollectionRemoveEventListeners = this.listeners[listenerType].Cast<IPostCollectionRemoveEventListener>().ToArray();
						break;

					case ListenerType.PostCollectionUpdate:
						cfg.EventListeners.PostCollectionUpdateEventListeners = this.listeners[listenerType].Cast<IPostCollectionUpdateEventListener>().ToArray();
						break;
				}
			}
		}

		/// <summary>
		/// Adds the listeners to the interal store for application to the Configuration
		/// </summary>
		/// <param name="type"></param>
		/// <param name="listeners"></param>
		/// <returns></returns>
		internal CustomListenerConfiguration AddListeners(ListenerType type, IEnumerable<object> listeners)
		{
			if (!this.listeners.ContainsKey(type))
				this.listeners.Add(type, new List<object>());

			foreach (var listener in listeners)
				this.listeners[type].Add(listener);

			return this;
		}
	}
}