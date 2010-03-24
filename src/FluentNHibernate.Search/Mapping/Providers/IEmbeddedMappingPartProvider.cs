using NHibernate.Search.Mapping;

namespace FluentNHibernate.Search.Mapping.Providers
{
	public interface IEmbeddedMappingPartProvider
	{
		void Apply(DocumentMapping mapping);
	}
}