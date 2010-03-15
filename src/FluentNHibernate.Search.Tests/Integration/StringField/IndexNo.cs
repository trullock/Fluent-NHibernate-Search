using System.Collections.Generic;
using FluentNHibernate.Search.Tests.Domain;
using FluentNHibernate.Search.Tests.Extensions;

namespace FluentNHibernate.Search.Tests.Integration.StringField
{
    public class IndexNo : TestDocumentIntegrationSpecification
    {
        private TestDocument document;
        private IList<TestDocument> results;

        protected override void Given()
        {
            document = new TestDocument {StringProperty = "lorem ipsum dolor sit amet"};
            session.Save(document);
            session.Flush();
        }

        protected override void When()
        {
            results = fullTextSession
                .CreateFullTextQuery<TestDocument>("StringProperty:dolor")
                .List<TestDocument>();
        }

        [Then]
        public void The_document_should_not_be_found()
        {
            results.Count.ShouldEqual(0);
        }

        protected override void configureSearchMapping()
        {
            Id(d => d.Id);
            Map(d => d.StringProperty)
                .Index().No();
        }
    }
}