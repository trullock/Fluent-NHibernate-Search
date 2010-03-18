namespace FluentNHibernate.Search.Cfg
{
    public class FluentSearchIndexingStrategyConfiguration
    {
        private readonly FluentSearchConfiguration cfg;

        internal FluentSearchIndexingStrategyConfiguration(FluentSearchConfiguration cfg)
        {
            this.cfg = cfg;
        }

        private FluentSearchConfiguration setStrategy(string name)
        {
			(cfg as IFluentSearchConfiguration).Configuration.Properties.Add(NHibernate.Search.Environment.IndexingStrategy, name);
            return cfg;
        }

        /// <summary>
        /// Sets the IndexingStrategy property to "manual"
        /// </summary>
        /// <returns></returns>
        public FluentSearchConfiguration Manual()
        {
            return setStrategy("manual");
        }

        /// <summary>
        /// Sets the IndexingStrategy property to "event"
        /// </summary>
        /// <returns></returns>
        public FluentSearchConfiguration Event()
        {
            return setStrategy("event");
        }
    }
}