using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Search.Bridge;
using FluentNHibernate.Search.Tests.Domain;
using Lucene.Net.Documents;

namespace FluentNHibernate.Search.Tests.Mappings
{
	public class TagBridge : IFieldBridge
	{
		public void Set(string name, object value, Lucene.Net.Documents.Document document, Lucene.Net.Documents.Field.Store store, Lucene.Net.Documents.Field.Index index, float? boost)
		{
			var tags = value as ICollection<Tag>;
			if (tags == null)
				return;

			foreach (var tag in tags)
			{
				var field = new Field(name, tag.Name, store, index);
				if (boost.HasValue)
					field.SetBoost(boost.Value);
				document.Add(field);
			}
		}
	}
}
