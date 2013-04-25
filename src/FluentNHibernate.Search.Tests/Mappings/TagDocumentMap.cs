using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Search.Tests.Domain;
using FluentNHibernate.Mapping;

namespace FluentNHibernate.Search.Tests.Mappings
{
	public class TagDocumentMap : ClassMap<Tag>
	{
		public TagDocumentMap()
		{
			Id(t => t.TagId);
			Map(t => t.Name);
		}

	}
}
