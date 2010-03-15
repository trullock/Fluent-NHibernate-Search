using FluentNHibernate.Search.Cfg;
using FluentNHibernate.Search.Tests.Extensions;
using NHibernate.Cfg;
using NHibernate.Search.Event;

namespace FluentNHibernate.Search.Tests.Unit.Listeners
{
	public class Default : Specification
	{
		private Configuration configuration;

		protected override void When()
		{
			this.configuration = FluentSearch.Configure()
				.Listeners(ListenerConfiguration.Default)
				.BuildConfiguration();
		}

		[Then]
		public void the_configuration_should_have_the_default_6_listeners_registered()
		{
			configuration.EventListeners.PostUpdateEventListeners[0].ShouldBeOfType<FullTextIndexEventListener>();
			configuration.EventListeners.PostDeleteEventListeners[0].ShouldBeOfType<FullTextIndexEventListener>();
			configuration.EventListeners.PostInsertEventListeners[0].ShouldBeOfType<FullTextIndexEventListener>();
		}
	}
}