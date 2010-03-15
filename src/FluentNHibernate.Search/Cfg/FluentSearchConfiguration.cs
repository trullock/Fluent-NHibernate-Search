using System;
using FluentNHibernate.Search.Mapping.Parts;
using NHibernate.Cfg;

namespace FluentNHibernate.Search.Cfg
{
	public class FluentSearchConfiguration : IFluentSearchConfiguration, IHasAnalyzer
	{
        public const string SearchCfgDefaultRoot = "hibernate.search.default.";
        public const string SearchCfgMappingClass = NHibernate.Search.Environment.MappingClass;
        public const string SearchCfgIndexBase = SearchCfgDefaultRoot + NHibernate.Search.Environment.IndexBase;
        public const string SearchCfgIndexingStrategy = NHibernate.Search.Environment.IndexingStrategy;
        public const string SearchCfgDirectoryProvider = SearchCfgDefaultRoot + "directory_provider";

		protected Configuration cfg { get; set; }

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
		Configuration IFluentSearchConfiguration.Configuration { get { return this.cfg; } }

		public FluentSearchConfiguration() : this(new Configuration())
		{
		}

		public FluentSearchConfiguration(Configuration cfg)
		{
			this.cfg = cfg;
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