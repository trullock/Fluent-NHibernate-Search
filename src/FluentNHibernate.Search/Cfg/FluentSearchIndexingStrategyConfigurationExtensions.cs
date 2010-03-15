namespace FluentNHibernate.Search.Cfg
{
    public static class FluentSearchIndexingStrategyConfigurationExtensions
    {
        /// <summary>
        /// Fluently set the IndexingStrategy property
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static FluentSearchIndexingStrategyConfiguration IndexingStrategy(this FluentSearchConfiguration self)
        {
            return new FluentSearchIndexingStrategyConfiguration(self);
        }

        /// <summary>
        /// Sets the IndexingStrategy property to the given string. This is a shrotcut for IndexingStrategy().Custom()
        /// </summary>
        /// <param name="self"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static FluentSearchConfiguration IndexingStrategy(this FluentSearchConfiguration self, string name)
        {
            return self.IndexingStrategy().Custom(name);
        }
    }
}