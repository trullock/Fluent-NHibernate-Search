using FluentNHibernate.Search.Mapping.Parts;

// Deliberately "incorrect" to aid discoverability
namespace FluentNHibernate.Search.Mapping
{
	public static class IFluentMappingPartExtensions
	{
		public static T Name<T>(this T self, string name) where T : IFluentMappingPart
		{
			self.Name = name;
			return self;
		}

		public static T Boost<T>(this T self, float? boost) where T : IFluentMappingPart
		{
			self.Boost = boost;
			return self;
		}
	}
}