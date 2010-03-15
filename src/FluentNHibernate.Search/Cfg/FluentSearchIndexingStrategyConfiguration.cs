namespace FluentNHibernate.Search.Cfg
{
    public class FluentSearchIndexingStrategyConfiguration
    {
        private readonly FluentSearchConfiguration cfg;

        internal FluentSearchIndexingStrategyConfiguration(FluentSearchConfiguration cfg)
        {
            this.cfg = cfg;
        }

        /// <summary>
        /// Sets the IndexingStrategy property to the given string
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public FluentSearchConfiguration Custom(string name)
        {
            (cfg as IFluentSearchConfiguration).Configuration.Properties.Add(FluentSearchConfiguration.SearchCfgIndexingStrategy, name);
            return cfg;
        }

        /// <summary>
        /// Sets the IndexingStrategy property to "manual"
        /// </summary>
        /// <returns></returns>
        public FluentSearchConfiguration Manual()
        {
            return Custom("manual");
        }

        /// <summary>
        /// Sets the IndexingStrategy property to "event"
        /// </summary>
        /// <returns></returns>
        public FluentSearchConfiguration Event()
        {
            return Custom("event");
        }
    }
}