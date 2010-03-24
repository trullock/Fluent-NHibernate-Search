using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FluentNHibernate.Search.Mapping.Providers;
using FluentNHibernate.Search.Reflection;
using NHibernate.Search.Mapping;

namespace FluentNHibernate.Search.Mapping.Parts
{
	public class EmbeddedMappingPart<TEmbedded> : IEmbeddedMappingPartProvider
	{
		private readonly IList<IFieldMappingProvider> fieldParts;

		public EmbeddedMappingPart()
		{
			fieldParts = new List<IFieldMappingProvider>();
		}

		/// <summary>
		/// Map a Property as a Lucene.NET Field.
		/// </summary>
		/// <param name="property"></param>
		/// <returns></returns>
		public FieldMappingPart Map(Expression<Func<TEmbedded, object>> property)
		{
			var field = new FieldMappingPart(property.ToPropertyInfo());
			this.fieldParts.Add(field);
			return field;
		}

		public void Apply(DocumentMapping mapping)
		{
			foreach(var part in this.fieldParts)
				mapping.Fields.Add(part.GetMapping());
		}
	}
}