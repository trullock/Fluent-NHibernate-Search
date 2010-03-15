using System;
using FluentNHibernate.Search.Cfg.EventListeners;
using FluentNHibernate.Search.Exceptions;
using FluentNHibernate.Search.Mapping.Parts;
using Lucene.Net.Analysis;
using NHibernate.Cfg;
using NHibernate.Search.Mapping;

namespace FluentNHibernate.Search.Cfg
{
	public static class FluentSearchConfigurationExtensions
	{
		/// <summary>
		/// Specifies the IndexBase proeprty
		/// </summary>
		/// <param name="self"></param>
		/// <param name="path"></param>
		/// <returns></returns>
		public static FluentSearchConfiguration IndexBase(this FluentSearchConfiguration self, string path)
		{
			if (string.IsNullOrEmpty(path))
				throw new ConfigurationException("IndexBase cannot be null or empty");

			(self as IFluentSearchConfiguration).Configuration.Properties.Add("hibernate.search.default." + NHibernate.Search.Environment.IndexBase, path);

			return self;
		}

		/// <summary>
		/// Specifies the MappingClass property
		/// </summary>
		/// <param name="self"></param>
		/// <returns></returns>
		public static FluentSearchConfiguration MappingClass<TSearchMapping>(this FluentSearchConfiguration self) where TSearchMapping : ISearchMapping
		{
			return self.MappingClass(typeof (TSearchMapping));
		}

		/// <summary>
		/// Specifies the MappingClass property
		/// </summary>
		/// <param name="self"></param>
		/// <param name="mappingClassType"></param>
		/// <returns></returns>
		public static FluentSearchConfiguration MappingClass(this FluentSearchConfiguration self, Type mappingClassType)
		{
			if(!typeof(ISearchMapping).IsAssignableFrom(mappingClassType))
				throw new ArgumentException("Must implement ISearchMapping", "mappingClassType");

			(self as IFluentSearchConfiguration).Configuration.Properties.Add(NHibernate.Search.Environment.MappingClass, mappingClassType.AssemblyQualifiedName);
			return self;
		}

        public static AnalyzerPart<FluentSearchConfiguration> DefaultAnalyzer(this FluentSearchConfiguration self)
        {
            return new AnalyzerPart<FluentSearchConfiguration>(self);
        }

        /// <summary>
        /// Defines the Default Analyzer property
        /// Shortcut for DefaultAnalyzer().Custom&lt;TAnalyzer&gt;()
        /// </summary>
        /// <typeparam name="TAnalyzer"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static FluentSearchConfiguration DefaultAnalyzer<TAnalyzer>(this FluentSearchConfiguration self) where TAnalyzer : Analyzer, new()
        {
            return DefaultAnalyzer(self).Custom<TAnalyzer>();
        }

		public static IFluentSearchConfiguration Listeners(this FluentSearchConfiguration self, IListenerConfiguration config)
		{
			config.Apply((self as IFluentSearchConfiguration).Configuration);
			return self;
		}
	}
}