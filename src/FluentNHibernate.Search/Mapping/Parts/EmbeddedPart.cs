using System;
using System.Reflection;
using FluentNHibernate.Search.Mapping.Providers;
using NHibernate.Properties;
using NHibernate.Search.Mapping;

namespace FluentNHibernate.Search.Mapping.Parts
{
	public class EmbeddedPart<TEmbedded> : IEmbeddedMappingProvider
	{
		public string prefix { get; protected set; }
		private bool isCollection;
		private BasicPropertyAccessor.BasicGetter getter;
		private readonly Type embeddedType;
		private IEmbeddedMappingPartProvider embeddedMappings;

		public EmbeddedPart(PropertyInfo propertyInfo)
		{
			this.embeddedType = propertyInfo.PropertyType;
			this.getter = new BasicPropertyAccessor.BasicGetter(propertyInfo.DeclaringType, propertyInfo, propertyInfo.Name);
		}

		/// <summary>
		/// Defines the Embedded Prefix.
		/// </summary>
		/// <param name="prefix"></param>
		/// <returns></returns>
		public EmbeddedPart<TEmbedded> Prefix(string prefix)
		{
			this.prefix = prefix;
			return this;
		}

		/// <summary>
		/// Defines the Embedded Depth
		/// Should not be used, not supported in current NHibernate.Search
		/// </summary>
		/// <param name="depth"></param>
		/// <returns></returns>
		[Obsolete("Should not be used, not supported in current NHibernate.Search")]
		public EmbeddedPart<TEmbedded> Depth(int depth)
		{
			throw new NotSupportedException();
		}

		/// <summary>
		/// Defines the Embedded Target Type
		/// Should not be used, not supported in current NHibernate.Search 
		/// </summary>
		/// <typeparam name="TTarget"></typeparam>
		/// <returns></returns>
		[Obsolete("Should not be used, not supported in current NHibernate.Search")]
		public EmbeddedPart<TEmbedded> Target<TTarget>()
		{
			throw new NotSupportedException();
		}

		/// <summary>
		/// Defines Embedded Field as Collection.
		/// </summary>
		/// <returns></returns>
		public EmbeddedPart<TEmbedded> AsCollection()
		{
			this.isCollection = true;
			return this;
		}

		public void AssertIsValid()
		{
		}

		public EmbeddedPart<TEmbedded> Mappings(Action<EmbeddedMappingPart<TEmbedded>> mappings)
		{
			var part = new EmbeddedMappingPart<TEmbedded>();
			mappings(part);
			this.embeddedMappings = part;
			return this;
		}

		public EmbeddedMapping GetMapping()
		{
			var documentMapping = new DocumentMapping(this.embeddedType);
			if (this.embeddedMappings != null)
				this.embeddedMappings.Apply(documentMapping);

			var mapping = new EmbeddedMapping(documentMapping, getter)
			              	{
			              		Prefix = this.prefix,
			              		IsCollection = this.isCollection
			              	};
			return mapping;
		}
	}
}