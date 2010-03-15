using System.Reflection;
using FluentNHibernate.Search.Mapping;
using FluentNHibernate.Search.Tests.Domain;
using FluentNHibernate.Search.Tests.Extensions;
using NHibernate.Properties;
using NHibernate.Search.Attributes;
using NHibernate.Search.Bridge;
using NHibernate.Search.Mapping;

namespace FluentNHibernate.Search.Tests.Unit.Field
{
    public class CustomName : Specification
    {
        private TestDocumentMapping mapping;
        private DocumentMapping documentMapping;
        private MethodInfo getMethod;

        public override void Given()
        {
            this.mapping = new TestDocumentMapping();

            getMethod = typeof(TestDocument).GetProperty("StringProperty").GetGetMethod();
        }

        protected override void When()
        {
            this.documentMapping = this.mapping.GetMapping();
        }

        [Then]
        public void the_document_map_should_have_a_field_set()
        {
            documentMapping.Fields.Count.ShouldEqual(1);
        }

        [Then]
        public void the_field_should_be_named_custom()
        {
            documentMapping.Fields[0].Name.ShouldEqual("custom");
        }

        [Then]
        public void the_field_should_have_a_twoway_string_bridge()
        {
            documentMapping.DocumentId.Bridge.ShouldBeOfType<TwoWayString2FieldBridgeAdaptor>();
        }

        [Then]
        public void the_field_should_be_index_tokenised()
        {
            documentMapping.Fields[0].Index.ShouldEqual(Index.Tokenized);
        }

        [Then]
        public void the_field_should_be_store_no()
        {
            documentMapping.Fields[0].Store.ShouldEqual(Store.No);
        }

        [Then]
        public void the_field_should_have_the_correct_getter()
        {
            var getter = documentMapping.Fields[0].Getter;

            getter.ShouldBeOfType<BasicPropertyAccessor.BasicGetter>();
            getter.PropertyName.ShouldEqual("StringProperty");
            getter.Method.ShouldEqual(getMethod);
            getter.ReturnType.ShouldEqual(typeof(string));
        }

        public class TestDocumentMapping : DocumentMap<TestDocument>
        {
            protected override void Configure()
            {
                Id(d => d.Id);
                Map(d => d.StringProperty).Name("custom");
            }
        }
    }
}