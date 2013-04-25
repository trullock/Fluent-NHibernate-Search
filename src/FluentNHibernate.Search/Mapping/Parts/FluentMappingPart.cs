using System.Reflection;
using NHibernate.Properties;
using NHibernate.Search.Bridge;

namespace FluentNHibernate.Search.Mapping.Parts
{
	public interface IFluentMappingPart
	{
		string Name { get; set; }
		float? Boost { get; set; }
	}

	public abstract class FluentMappingPart : IFluentMappingPart, IHasBridge
	{
		string IFluentMappingPart.Name { get; set; }
		float? IFluentMappingPart.Boost { get; set; }
		IFieldBridge IHasBridge.FieldBridge { get; set; }
        public IGetter Getter { get; set; }

		protected FluentMappingPart(PropertyInfo propertyInfo)
		{
			this.Name(propertyInfo.Name);

			// set the default getter
			this.Getter = new BasicPropertyAccessor.BasicGetter(propertyInfo.DeclaringType, propertyInfo, propertyInfo.Name);

			try
			{
				// set the default bridge
				var bridge = BridgeFactory.GuessType(propertyInfo.Name, propertyInfo.PropertyType, null, null);
				(this as IHasBridge).FieldBridge = bridge;
			}
			catch
			{ }
		}
	}
}