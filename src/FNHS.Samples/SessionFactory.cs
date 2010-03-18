using System.Data;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Search.Cfg;
using FNHS.Samples.Mappings;
using FNHS.Samples.SearchMappings;
using NHibernate;
using NHibernate.ByteCode.Castle;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Event;
using NHibernate.Search.Event;
using NHibernate.Tool.hbm2ddl;

namespace FNHS.Samples
{
    public class SessionFactory
    {
        private const string ConnectionString = "Data Source=:memory:;Version=3;New=True;";
        private static ISessionFactory _sessionFactory;
        private static Configuration _cfg;

        static SessionFactory()
        {
            CreateSessionFactory();
        }

        private static void CreateSessionFactory()
        {
            // FluentNHibernate.Search Configuration
            Configuration fnhscfg = FluentSearch.Configure()
                .DefaultAnalyzer().Standard()
                .DirectoryProvider().RAMDirectory()
                .IndexingStrategy().Event()
				.Listeners(ListenerConfiguration.Default)
                .MappingClass<SearchMap>()
                .BuildConfiguration();

            // FluentNHibernate Configuration
            Configuration fnhcfg = Fluently.Configure(fnhscfg)
                    .Database(SQLiteConfiguration.Standard.InMemory()
                    .Dialect<SQLiteDialect>()
                    .ConnectionString(ConnectionString)
                    .ProxyFactoryFactory<ProxyFactoryFactory>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<AuthorMap>())
                .ExposeConfiguration(cfg =>
                                         {
                                             cfg.SetListeners(ListenerType.PostInsert, new[] { new FullTextIndexEventListener() });
                                             cfg.SetListeners(ListenerType.PostUpdate, new[] { new FullTextIndexEventListener() });
                                             cfg.SetListeners(ListenerType.PostDelete, new[] { new FullTextIndexEventListener() });

                                             cfg.SetListener(ListenerType.PostCollectionRecreate, new FullTextIndexCollectionEventListener());
                                             cfg.SetListener(ListenerType.PostCollectionRemove, new FullTextIndexCollectionEventListener());
                                             cfg.SetListener(ListenerType.PostCollectionUpdate, new FullTextIndexCollectionEventListener());
                                         })
                .BuildConfiguration();

            _cfg = fnhcfg;
            _sessionFactory = _cfg.BuildSessionFactory();
        }

        public ISession CreateSession()
        {
            ISession openSession = _sessionFactory.OpenSession();
            IDbConnection connection = openSession.Connection;
            new SchemaExport(_cfg).Execute(false, true, false, connection, null);
            return openSession;
        }
    }
}