using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Search.Tests.Mappings;
using FluentNHibernate.Search.Tests.Domain;
using FluentNHibernate.Search.Tests.Extensions;

namespace FluentNHibernate.Search.Tests.Integration.Bridge
{
	public class CustomBridge : TestDocumentIntegrationSpecification
	{
		private TestDocument document;
		private IList<TestDocument> results;

		protected override void configureSearchMapping()
		{
			Id(d => d.Id);
			Map(d => d.StringProperty)
					.Index().No();
			Map(d => d.Tags)
				.Index().Tokenized()
				.Store().Yes()
				.Bridge().Custom<TagBridge>();
		}

		protected override void Given()
		{
			document = new TestDocument { StringProperty = "lorem ipsum dolor sit amet" };
			var fiction = new Tag { Name = "Fiction" };
			var latin = new Tag { Name = "Latin" };
			document.Tags.Add(fiction);
			document.Tags.Add(latin);

			session.Save(fiction);
			session.Save(latin);
			session.Save(document);
			session.Flush();
		}

		protected override void When()
		{
			results = fullTextSession
					.CreateFullTextQuery<TestDocument>("Tags:latin")
					.List<TestDocument>();
		}

		[Then]
		public void The_document_should_be_found()
		{
			results.Count.ShouldEqual(1);
		}
	}
}
