using System;
using System.Reflection;
using FluentNHibernate.Search.Exceptions;
using FluentNHibernate.Search.Mapping.Providers;
using Lucene.Net.Analysis;
using NHibernate.Properties;
using NHibernate.Search.Attributes;
using NHibernate.Search.Bridge;
using NHibernate.Search.Mapping;

namespace FluentNHibernate.Search.Mapping.Parts
{
    public class FieldMappingPart : FluentMappingPart, IFieldMappingProvider, IHasBridge, IHasAnalyzer, IHasIndex, IHasStore
    {
        Index? IHasIndex.Index { get; set; }
        Store? IHasStore.Store { get; set; }
        Type IHasAnalyzer.AnalyzerType { get; set; }
        IFieldBridge IHasBridge.FieldBridge { get; set; }

        public FieldMappingPart(PropertyInfo propertyInfo) : base(propertyInfo)
        {
        }

        /// <summary>
        /// Defines the Field Index.
        /// </summary>
        /// <returns></returns>
        public IndexPart<FieldMappingPart> Index()
        {
            return new IndexPart<FieldMappingPart>(this);
        }

        /// <summary>
        /// Defines the Field Store.
        /// </summary>
        /// <returns></returns>
        public StorePart<FieldMappingPart> Store()
        {
            return new StorePart<FieldMappingPart>(this);
        }

        /// <summary>
        /// Defines the Field Bridge.
        /// </summary>
        /// <returns></returns>
        public BridgePart<FieldMappingPart> Bridge()
        {
            return new BridgePart<FieldMappingPart>(this);
        }

        /// <summary>
        /// Defines the Field Analyzer.
        /// </summary>
        /// <returns></returns>
        public AnalyzerPart<FieldMappingPart> Analyzer()
        {
            return new AnalyzerPart<FieldMappingPart>(this);
        }

        public FieldMapping GetMapping()
        {
            var mappingPart = this as IFluentMappingPart;

            var mapping = new FieldMapping(mappingPart.Name, (this as IHasBridge).FieldBridge, this.Getter)
                              {
                                  Boost = mappingPart.Boost
                              };

            var hasStore = this as IHasStore;
            if (hasStore.Store.HasValue)
                mapping.Store = hasStore.Store.Value;

            var hasIndex = this as IHasIndex;
            if (hasIndex.Index.HasValue)
                mapping.Index = hasIndex.Index.Value;

            var hasAnalyzer = this as IHasAnalyzer;
            if (hasAnalyzer.AnalyzerType != null)
                mapping.Analyzer = Activator.CreateInstance(hasAnalyzer.AnalyzerType) as Analyzer;

            return mapping;
        }

        public void AssertIsValid()
        {
            if (string.IsNullOrEmpty((this as IFluentMappingPart).Name))
                throw new FieldMappingException("Name cannot be null");

            if ((this as IHasBridge).FieldBridge == null)
                throw new FieldMappingException("Bridge cannot be null");
        }
    }
}