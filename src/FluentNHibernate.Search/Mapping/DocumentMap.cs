using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using FluentNHibernate.Search.Exceptions;
using FluentNHibernate.Search.Mapping.Parts;
using FluentNHibernate.Search.Mapping.Providers;
using FluentNHibernate.Search.Reflection;
using Lucene.Net.Analysis;
using NHibernate.Properties;
using NHibernate.Search.Mapping;

namespace FluentNHibernate.Search.Mapping
{
    /// <summary>
    /// Defines the Lucene.NET Class Mapping
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class DocumentMap<T> : IDocumentMappingProvider, IHasAnalyzer, IFluentMappingPart
    {
		string IFluentMappingPart.Name { get; set; }
		float? IFluentMappingPart.Boost { get; set; }
		Type IHasAnalyzer.AnalyzerType { get; set; }

		private DocumentIdMappingPart idMappingPart;
		private readonly IList<FieldMappingPart> fieldParts;
    	private readonly IList<EmbeddedPart<T>> embeddedParts;
		private readonly IList<ContainedInMapping> containedInParts;
    	private readonly IList<DocumentBridgePart<T>> documentBridgeParts;

    	private int level;
    	private readonly int maxLevel;

        protected DocumentMap()
        {
            this.fieldParts = new List<FieldMappingPart>();
            this.embeddedParts = new List<EmbeddedPart<T>>();
			this.containedInParts = new List<ContainedInMapping>();
            this.documentBridgeParts = new List<DocumentBridgePart<T>>();

            this.maxLevel = int.MaxValue;
        	this.Name(typeof (T).Name);
        }

        protected abstract void Configure();

        /// <summary>
        /// Defines the DocumentId Mapping.
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        protected DocumentIdMappingPart Id(Expression<Func<T, object>> property)
        {
            this.idMappingPart = new DocumentIdMappingPart(property.ToPropertyInfo());
            return this.idMappingPart;
        }

        /// <summary>
        /// Map a Property as a Lucene.NET Field.
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        protected FieldMappingPart Map(Expression<Func<T, object>> property)
        {
            var field = new FieldMappingPart(property.ToPropertyInfo());
            this.fieldParts.Add(field);
            return field;
        }

        /// <summary>
        /// Defines the Lucene.NET Index Name.
        /// </summary>
        /// <param name="name"></param>
        protected void Name(string name)
        {
            (this as IFluentMappingPart).Name = name;
        }

        /// <summary>
        /// Defines the Default Analyzer for this mapped class.
        /// </summary>
        /// <returns></returns>
		protected AnalyzerPart<DocumentMap<T>> Analyzer()
        {
			return new AnalyzerPart<DocumentMap<T>>(this);
        }

        /// <summary>
        /// Sets the Analyzer to the given Analyzer type.
        /// Shortcut for .Analyzer().Custom&lt;TAnalyzer&gt;
        /// </summary>
        /// <typeparam name="TAnalyzer"></typeparam>
        /// <returns></returns>
        protected DocumentMap<T> Analyzer<TAnalyzer>() where TAnalyzer : Analyzer, new()
        {
            return this.Analyzer().Custom<TAnalyzer>();
        }

        /// <summary>
        /// Sets the boost value
        /// </summary>
        /// <param name="boost"></param>
        protected void Boost(float? boost)
		{
            (this as IFluentMappingPart).Boost = boost;
        }

        [Obsolete("Not currently guaranteed to work")]
        protected EmbeddedPart<T> Embedded(Expression<Func<T, object>> property)
        {
            var part = new EmbeddedPart<T>(property);
            this.embeddedParts.Add(part);

            return part;
        }

        [Obsolete("Not currently guaranteed to work")]
		protected DocumentMap<T> ContainedIn(Expression<Func<T, object>> property)
        {
			var propertyInfo = property.ToPropertyInfo();
			var getter = new BasicPropertyAccessor.BasicGetter(typeof(T), propertyInfo, propertyInfo.Name);
			var mapping = new ContainedInMapping(getter);
			this.containedInParts.Add(mapping);

            return this;
        }

        protected DocumentBridgePart<T> Bridge()
        {
            var part = new DocumentBridgePart<T>();
            this.documentBridgeParts.Add(part);
            return part;
        }

        public DocumentMapping GetMapping()
        {
			Configure();

        	var mappingPart = this as IFluentMappingPart;

            var doc = new DocumentMapping(typeof(T))
            {
                DocumentId = this.idMappingPart.GetMapping(),
				Boost = mappingPart.Boost,
				IndexName = mappingPart.Name ?? typeof(T).Name,
            };

            var context = new BuildContext
                              {
                                  Root = doc,
                                  Processed = { typeof(T) }
                              };

        	var hasAnalyzer = this as IHasAnalyzer;
        	if (hasAnalyzer.AnalyzerType != null)
        		doc.Analyzer = Activator.CreateInstance(hasAnalyzer.AnalyzerType) as Analyzer;

        	BuildMapping(doc, context);

            return doc;
        }

        public void AssertIsValid()
        {
            Configure();

        	if (string.IsNullOrEmpty((this as IFluentMappingPart).Name))
        		throw new DocumentMappingException("Index name cannot be null");

        	if (this.idMappingPart == null)
        		throw new DocumentMappingException("Document Id cannot be null");
        }

        internal class BuildContext
        {
            public BuildContext()
            {
                Processed = new List<Type>();
            }

            public DocumentMapping Root { get; set; }
            public IList<Type> Processed { get; private set; }
        }

        private void BuildMapping(DocumentMapping doc, BuildContext context)
        {
        	foreach (var part in this.fieldParts)
        		doc.Fields.Add(part.GetMapping());

        	foreach (var part in this.documentBridgeParts)
        		doc.ClassBridges.Add(part.GetMapping());

        	foreach (var part in this.embeddedParts)
        		BuildEmbedded(doc, part, context);

        	foreach (var part in this.containedInParts)
        		doc.ContainedIn.Add(part);
        }


        /// <summary>
        /// TODO: Remimplement circular reference prevention
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="part"></param>
        /// <param name="context"></param>
        private void BuildEmbedded(DocumentMapping doc, EmbeddedPart<T> part, BuildContext context)
        {
            var oldMaxLevel = this.maxLevel;
            var embedded = part;
           // var property = part.Property.ToPropertyInfo();
            var elementType = part.GetType().GetGenericArguments()[0];

            this.level++;

            //var localPrefix = embedded.InternalPrefix == "." ? property.Name + "." : embedded.InternalPrefix;

        	//if (this.maxLevel == int.MaxValue && context.Processed.Contains(elementType))
        	//	throw new Exception("Circular reference");

        	//if (this.level <= this.maxLevel)
           // {
            //    context.Processed.Add(elementType);

                var map = embedded.GetMapping();

               // map.Prefix = localPrefix;

            	if (typeof (IEnumerable).IsAssignableFrom(elementType))
            		map.IsCollection = true;

            	doc.Embedded.Add(map);

                //var actualType = property.PropertyType;
                //context.Processed.Remove(actualType);
           // }

            //this.level--;
           // this.maxLevel = oldMaxLevel;
        }
    }
}