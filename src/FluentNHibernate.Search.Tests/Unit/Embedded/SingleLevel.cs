using FluentNHibernate.Search.Mapping;
using FluentNHibernate.Search.Tests.Domain;
using FluentNHibernate.Search.Tests.Extensions;
using NHibernate.Search.Mapping;

namespace FluentNHibernate.Search.Tests.Unit.Embedded
{
	public class SingleLevel : Specification
	{
		private AuthorMapping mapping;
		private DocumentMapping documentMapping;

		public override void Given()
		{
			this.mapping = new AuthorMapping();
		}

		protected override void When()
		{
			this.documentMapping = this.mapping.GetMapping();
		}

		[Then]
		public void the_document_map_should_have_an_embedded_mapping_on_it()
		{
			this.documentMapping.Embedded[0].ShouldNotBeNull();
		}

		[Then]
		public void the_embedded_document_map_should_have_the_mapped_field()
		{
			this.documentMapping.Embedded[0].Class.Fields[0].Name.ShouldEqual("Postcode");
		}

		public class AuthorMapping : DocumentMap<Author>
		{
			protected override void Configure()
			{
				Id(a => a.AuthorId);
				Embedded(a => a.Address)
					.Mappings(m => m.Map(a => a.Postcode));
			}
		}

	}
}