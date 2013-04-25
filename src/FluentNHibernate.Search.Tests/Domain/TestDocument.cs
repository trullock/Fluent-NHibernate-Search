using System;
using System.Collections.Generic;

namespace FluentNHibernate.Search.Tests.Domain
{
    public class TestDocument
    {
        public Guid Id { get; set; }

        public string StringProperty { get; set; }

				public ICollection<Tag> Tags { get; private set; }

				public TestDocument()
				{
					Tags = new List<Tag>();
				}
    }
}