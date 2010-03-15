namespace FluentNHibernate.Search.Cfg
{
    public static class FluentSearchDirectoryProviderConfigurationExtensions
    {
        public static FluentSearchDirectoryProviderConfiguration DirectoryProvider(this FluentSearchConfiguration self)
        {
            return new FluentSearchDirectoryProviderConfiguration(self);
        }
    }
}