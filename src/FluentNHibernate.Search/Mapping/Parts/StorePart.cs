using System;
using NHibernate.Search.Attributes;

namespace FluentNHibernate.Search.Mapping.Parts
{
    public class StorePart<T> where T : IHasStore
    {
		private readonly T hasStore;

		public StorePart(T hasStore)
        {
            this.hasStore = hasStore;
        }

		private T store(Store store)
        {
			hasStore.Store = store;
			return this.hasStore;
        }

        /// <summary>
        /// Sets the Store mode to Yes
        /// </summary>
        /// <returns></returns>
		public T Yes()
        {
            return store(Store.Yes);
        }

        /// <summary>
        /// Sets the Store mode to No
        /// </summary>
        /// <returns></returns>
		public T No()
        {
            return store(Store.No);
        }

        /// <summary>
        /// Sets the Store mode to Compress
        /// Should not be used, not implemented in NHibernate.Search.
        /// </summary>
        /// <returns></returns>
        [Obsolete("Should not be used, not implemented in NHibernate.Search")]
		public T Compress()
        {
            return store(Store.Compress);
        }
    }
}