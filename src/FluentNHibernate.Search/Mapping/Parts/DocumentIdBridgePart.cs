using NHibernate.Search.Bridge;

namespace FluentNHibernate.Search.Mapping.Parts
{
	public class DocumentIdBridgePart<T> : BridgePart<T> where T : IHasBridge
	{
		public DocumentIdBridgePart(T fieldMappingPart)
			: base(fieldMappingPart)
		{
		}

		public new T Custom<TBridge>(TBridge bridge) where TBridge : ITwoWayFieldBridge, new()
		{
			this.mappingPart.FieldBridge = bridge;
			return this.mappingPart;
		}

		public new T Custom<TBridge>() where TBridge : ITwoWayFieldBridge, new()
		{
			return this.Custom(new TBridge());
		}
	}
}