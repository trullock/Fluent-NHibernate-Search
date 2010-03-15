using System;
using FluentNHibernate.Cfg;
using FluentNHibernate.Search.Cfg;

namespace FluentNHibernate.Search
{
    public static class FluentNHibernateExtensions
    {
        public static FluentConfiguration Search(this FluentConfiguration self, Func<FluentSearchConfiguration, FluentSearchConfiguration> searchConfig)
        {
            return self.ExposeConfiguration(c => searchConfig(FluentSearch.Configure(c)));
        }
    }
}