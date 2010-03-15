using System;
using FluentNHibernate.Search.Exceptions;
using FluentNHibernate.Search.Mapping.Providers;
using Lucene.Net.Analysis;
using NHibernate.Search.Attributes;
using NHibernate.Search.Bridge;
using NHibernate.Search.Mapping;

namespace FluentNHibernate.Search.Mapping.Parts
{
	public class DocumentBridgePart<T> : IDocumentBridgeMappingProvider, IFluentMappingPart, IHasStore, IHasIndex, IHasAnalyzer, IHasBridge
	{
		string IFluentMappingPart.Name { get; set; }
		float? IFluentMappingPart.Boost { get; set; }
		IFieldBridge IHasBridge.FieldBridge { get; set; }
		Type IHasAnalyzer.AnalyzerType { get; set; }
		Store? IHasStore.Store { get; set; }
		Index? IHasIndex.Index { get; set; }

		public AnalyzerPart<DocumentBridgePart<T>> Analyzer()
		{
			return new AnalyzerPart<DocumentBridgePart<T>>(this);
		}

		public IndexPart<DocumentBridgePart<T>> Index()
		{
			return new IndexPart<DocumentBridgePart<T>>(this);
		}

		public StorePart<DocumentBridgePart<T>> Store()
		{
			return new StorePart<DocumentBridgePart<T>>(this);
		}

		public BridgePart<DocumentBridgePart<T>> Bridge()
		{
			return new BridgePart<DocumentBridgePart<T>>(this);
		}

		public ClassBridgeMapping GetMapping()
		{
			var mappingPart = this as IFluentMappingPart;

			var mapping = new ClassBridgeMapping(mappingPart.Name, (this as IHasBridge).FieldBridge)
			              	{
			              		Boost = mappingPart.Boost
			              	};

			var hasAnalyzer = this as IHasAnalyzer;
			if (hasAnalyzer.AnalyzerType != null)
				mapping.Analyzer = Activator.CreateInstance(hasAnalyzer.AnalyzerType) as Analyzer;


		    var hasStore = (this as IHasStore);
		    if (hasStore.Store.HasValue)
				mapping.Store = hasStore.Store.Value;


		    var hasIndex = (this as IHasIndex);
		    if (hasIndex.Index.HasValue)
				mapping.Index = hasIndex.Index.Value;

			return mapping;
		}

		public void AssertIsValid()
		{
			if (string.IsNullOrEmpty((this as IFluentMappingPart).Name))
				throw new DocumentBridgeMappingException("Nname cannot be null");

			if ((this as IHasBridge).FieldBridge == null)
				throw new DocumentBridgeMappingException("Bridge cannot be null");
		}		
	}
}