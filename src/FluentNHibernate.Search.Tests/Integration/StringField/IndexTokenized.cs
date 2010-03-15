using FluentNHibernate.Search.Tests.Domain;
using FluentNHibernate.Search.Tests.Extensions;

namespace FluentNHibernate.Search.Tests.Integration.StringField
{
    public class IndexTokenized : TestDocumentIntegrationSpecification
    {
        private TestDocument document;
        private TestDocument result;

        protected override void Given()
        {
            document = new TestDocument {StringProperty = "lorem ipsum dolor sit amet"};
            session.Save(document);
            session.Flush();
        }

        protected override void When()
        {
            result = fullTextSession
                .CreateFullTextQuery<TestDocument>("StringProperty:dolor")
                .List<TestDocument>()[0];
        }

        [Then]
        public void The_document_should_be_found()
        {
            result.ShouldEqual(document);
        }

        protected override void configureSearchMapping()
        {
            Id(d => d.Id);
            Map(d => d.StringProperty)
                .Index().Tokenized();
        }
    }
}