using System;
using FluentNHibernate.Search.Mapping;
using FluentNHibernate.Search.Tests.Domain;
using FluentNHibernate.Search.Tests.Extensions;
using NHibernate.Search.Bridge;
using NHibernate.Search.Mapping;

namespace FluentNHibernate.Search.Tests.Unit.DocumentId
{
    public class NoOptions : Specification
    {
        private TestDocumentMapping mapping;
        private DocumentMapping documentMapping;

        public override void Given()
        {
            this.mapping = new TestDocumentMapping();
        }

        protected override void When()
        {
            this.documentMapping = this.mapping.GetMapping();
        }

        [Then]
        public void the_document_map_should_have_a_documentid_property_set()
        {
            documentMapping.DocumentId.ShouldNotBeNull();
        }

        [Then]
        public void the_documentid_should_be_named_id()
        {
            documentMapping.DocumentId.Name.ShouldEqual("Id");
        }

        [Then]
        public void the_documentid_should_have_a_twoway_bridge()
        {
            documentMapping.DocumentId.Bridge.ShouldBeOfType<ITwoWayFieldBridge>();
        }

        public class TestDocumentMapping : DocumentMap<TestDocument>
        {
            protected override void Configure()
            {
                Id(d => d.Id);
            }
        }
    }
}