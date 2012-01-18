using System;
using System.Data;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Search.Cfg;
using FluentNHibernate.Search.Mapping;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Event;
using NHibernate.Search;
using NHibernate.Search.Event;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace FluentNHibernate.Search.Tests.Integration
{
	[TestFixture]
	public abstract class Integration_Specification<TDocument> : DocumentMap<TDocument>
	{
		protected ISession session;
		protected IFullTextSession fullTextSession;

		[SetUp]
		public void SetUp()
		{
			buildSession();
			Given();
			When();
		}

		private void buildSession()
		{
			var cfg = createConfig();
			var sessionFactory = cfg.BuildSessionFactory();

			this.session = sessionFactory.OpenSession();
			IDbConnection connection = session.Connection;
			new SchemaExport(cfg).Execute(false, true, false, connection, null);

			fullTextSession = NHibernate.Search.Search.CreateFullTextSession(session);
		}

		protected virtual void Given()
		{
		}

		protected abstract void When();

		private Configuration createConfig()
		{
			return searchConfig(FluentSearch.Configure(
				Fluently
					.Configure()
					.Database(SQLiteConfiguration.Standard.InMemory()
					.Dialect<SQLiteDialect>())
					.Mappings(fnhMappings)
					.ExposeConfiguration(cfg =>
					{
						cfg.SetListeners(ListenerType.PostInsert, new[] {new FullTextIndexEventListener()});
						cfg.SetListeners(ListenerType.PostUpdate, new[] {new FullTextIndexEventListener()});
						cfg.SetListeners(ListenerType.PostDelete, new[] {new FullTextIndexEventListener()});

						cfg.SetListener(ListenerType.PostCollectionRecreate, new FullTextIndexCollectionEventListener());
						cfg.SetListener(ListenerType.PostCollectionRemove, new FullTextIndexCollectionEventListener());
						cfg.SetListener(ListenerType.PostCollectionUpdate, new FullTextIndexCollectionEventListener());
					})
					.BuildConfiguration()));
		}

		protected abstract void fnhMappings(MappingConfiguration config);
		protected abstract Configuration searchConfig(FluentSearchConfiguration config);
	}
}