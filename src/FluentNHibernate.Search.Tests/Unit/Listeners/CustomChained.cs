using FluentNHibernate.Search.Cfg;
using FluentNHibernate.Search.Tests.Extensions;
using NHibernate.Cfg;
using NHibernate.Search.Event;

namespace FluentNHibernate.Search.Tests.Unit.Listeners
{
	public class CustomChained : Specification
	{
		private Configuration configuration;
		private FullTextIndexEventListener listener;

		public override void Given()
		{
			base.Given();

			this.listener = new FullTextIndexEventListener();
		}
		protected override void When()
		{
			this.configuration = FluentSearch.Configure()
				.Listeners(ListenerConfiguration.Custom
				           	.PostInsert(listener)
							.PostUpdate(listener))
				.BuildConfiguration();
		}

		[Then]
		public void the_configuration_should_have_both_custom_listeners_registered()
		{
			configuration.EventListeners.PostInsertEventListeners[0].ShouldEqual(listener);
			configuration.EventListeners.PostUpdateEventListeners[0].ShouldEqual(listener);
		}
	}
}