using System;
using System.Collections.Generic;
using FluentNHibernate.Search.Mapping.Parts;
using NHibernate.Cfg;

namespace FluentNHibernate.Search.Cfg
{
	public class FluentSearchConfiguration : IFluentSearchConfiguration, IHasAnalyzer
	{
		protected Configuration cfg { get; set; }

		Configuration IFluentSearchConfiguration.Configuration
		{
			get { return this.cfg; }
		}

		Type IHasAnalyzer.AnalyzerType
		{
			get
			{
				if (!cfg.Properties.ContainsKey(NHibernate.Search.Environment.AnalyzerClass))
					return null;
				return Type.GetType(cfg.Properties[NHibernate.Search.Environment.AnalyzerClass]);
			}
			set { this.cfg.Properties[NHibernate.Search.Environment.AnalyzerClass] = value.AssemblyQualifiedName; }
		}

		IDictionary<string, string> IFluentSearchConfiguration.Properties
		{
			get { return this.cfg.Properties; }
		}

		public FluentSearchConfiguration() : this(new Configuration())
		{
		}

		public FluentSearchConfiguration(Configuration cfg)
		{
			this.cfg = cfg;
		}

		public IFluentSearchConfiguration Listeners(Func<ListenersPart, ListenersPart> listenerConfig)
		{
			listenerConfig(new ListenersPart(this));
			return this;
		}

		/// <summary>
		/// Exposes the underlying NHibernate.Cfg.Configuration for custom modifications
		/// </summary>
		/// <param name="alteration"></param>
		/// <returns></returns>
		public IFluentSearchConfiguration ExposeConfiguration(Action<Configuration> alteration)
		{
			alteration(cfg);
			return this;
		}

		public Configuration BuildConfiguration()
		{
			return cfg;
		}
	}
}