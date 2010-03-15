using NHibernate.Search.Attributes;

namespace FluentNHibernate.Search.Mapping.Parts
{
    public class IndexPart<T> where T : IHasIndex
    {
		private readonly T hasIndex;

		public IndexPart(T hasIndex)
        {
			this.hasIndex = hasIndex;
        }

		private T index(Index index)
        {
			this.hasIndex.Index = index;
			return hasIndex;
        }

        /// <summary>
        /// Sets the Index mode to No
        /// </summary>
        /// <returns></returns>
		public T No()
        {
            return index(Index.No);
        }

        /// <summary>
        /// Sets the Index mode to Tokenized
        /// </summary>
        /// <returns></returns>
		public T Tokenized()
        {
            return index(Index.Tokenized);
        }

        /// <summary>
        /// Sets the Index mode to UnTokenized
        /// </summary>
        /// <returns></returns>
		public T UnTokenized()
        {
            return index(Index.UnTokenized);
        }

        /// <summary>
        /// Sets the Index mode to NoForms
        /// </summary>
        /// <returns></returns>
		public T NoNorms()
        {
            return index(Index.NoNorms);
        }
    }
}