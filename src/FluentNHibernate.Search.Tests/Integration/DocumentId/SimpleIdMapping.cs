using System;
using System.Collections.Generic;
using FluentNHibernate.Cfg;
using FluentNHibernate.Search.Cfg;
using FluentNHibernate.Search.Mapping;
using FluentNHibernate.Search.Tests.Domain;
using FluentNHibernate.Search.Tests.Extensions;
using FluentNHibernate.Search.Tests.Mappings;
using NHibernate.Cfg;
using NHibernate.Search.Mapping;

namespace FluentNHibernate.Search.Tests.Integration.DocumentId
{
    public class SimpleIdMapping : TestDocumentIntegrationSpecification
    {
        private TestDocument document;
        private TestDocument result;

        protected override void Given()
        {
            document = new TestDocument();
            session.Save(document);
            session.Flush();
        }

        protected override void When()
        {
            result = fullTextSession
                .CreateFullTextQuery<TestDocument>("Id:" + document.Id)
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
        }
    }
}