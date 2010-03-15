using NHibernate.Cfg;

namespace FluentNHibernate.Search.Cfg
{
    public static class FluentSearch
    {
        /// <summary>
        /// Fluently configure NHibernate.Search Mappings.
        /// </summary>
        /// <returns></returns>
        public static FluentSearchConfiguration Configure()
        {
            return new FluentSearchConfiguration();
        }

        /// <summary>
        /// Fluently configure NHibernate.Search Mappings using the underlying NHibernate Configuration.
        /// </summary>
        /// <param name="cfg"></param>
        /// <returns></returns>
        public static FluentSearchConfiguration Configure(Configuration cfg)
        {
            return new FluentSearchConfiguration(cfg);
        }
    }
}