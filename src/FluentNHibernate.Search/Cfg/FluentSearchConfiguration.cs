using System;
using FluentNHibernate.Search.Cfg.EventListeners;
using FluentNHibernate.Search.Exceptions;
using FluentNHibernate.Search.Mapping.Parts;
using Lucene.Net.Analysis;
using NHibernate.Cfg;
using NHibernate.Search.Mapping;

namespace FluentNHibernate.Search.Cfg
{
	public class FluentSearchConfiguration : IFluentSearchConfiguration, IHasAnalyzer
	{
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

		internal FluentSearchConfiguration() : this(new Configuration())
		{
		}

		internal FluentSearchConfiguration(Configuration cfg)
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

		/// <summary>
		/// Specifies the IndexBase proeprty
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public FluentSearchConfiguration IndexBase(string path)
		{
			if (string.IsNullOrEmpty(path))
				throw new ConfigurationException("IndexBase cannot be null or empty");

			this.cfg.Properties.Add("hibernate.search.default." + NHibernate.Search.Environment.IndexBase, path);

			return this;
		}

		/// <summary>
		/// Specifies the MappingClass property
		/// </summary>
		/// <returns></returns>
		public FluentSearchConfiguration MappingClass<TSearchMapping>() where TSearchMapping : ISearchMapping
		{
			return this.MappingClass(typeof(TSearchMapping));
		}

		/// <summary>
		/// Specifies the MappingClass property
		/// </summary>
		/// <param name="mappingClassType"></param>
		/// <returns></returns>
		public FluentSearchConfiguration MappingClass(Type mappingClassType)
		{
			if (!typeof(ISearchMapping).IsAssignableFrom(mappingClassType))
				throw new ArgumentException("Must implement ISearchMapping", "mappingClassType");

			this.cfg.Properties.Add(NHibernate.Search.Environment.MappingClass, mappingClassType.AssemblyQualifiedName);
			return this;
		}

		/// <summary>
		/// Fluently set the Default Analyzer
		/// </summary>
		/// <returns></returns>
		public AnalyzerPart<FluentSearchConfiguration> DefaultAnalyzer()
		{
			return new AnalyzerPart<FluentSearchConfiguration>(this);
		}

		/// <summary>
		/// Defines the Default Analyzer property
		/// Shortcut for DefaultAnalyzer().Custom&lt;TAnalyzer&gt;()
		/// </summary>
		/// <typeparam name="TAnalyzer"></typeparam>
		/// <returns></returns>
		public FluentSearchConfiguration DefaultAnalyzer<TAnalyzer>() where TAnalyzer : Analyzer, new()
		{
			return DefaultAnalyzer().Custom<TAnalyzer>();
		}

		/// <summary>
		/// Fluently set the DirectoryProvider
		/// </summary>
		/// <returns></returns>
		public FluentSearchDirectoryProviderConfiguration DirectoryProvider()
		{
			return new FluentSearchDirectoryProviderConfiguration(this);
		}

		/// <summary>
		/// Fluently set the IndexingStrategy property
		/// </summary>
		/// <returns></returns>
		public FluentSearchIndexingStrategyConfiguration IndexingStrategy()
		{
			return new FluentSearchIndexingStrategyConfiguration(this);
		}

		/// <summary>
		/// Fluently set the Event Listeners
		/// </summary>
		/// <param name="config"></param>
		/// <returns></returns>
		public FluentSearchConfiguration Listeners(IListenerConfiguration config)
		{
			config.Apply(this.cfg);
			return this;
		}

		public Configuration BuildConfiguration()
		{
			return cfg;
		}
	}
}