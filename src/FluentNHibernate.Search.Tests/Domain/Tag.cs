using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentNHibernate.Search.Tests.Domain
{
	public class Tag
	{
		public virtual Guid TagId { get; set; }
		public virtual string Name { get; set; }
	}
}
