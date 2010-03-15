using NHibernate.Search.Bridge;

namespace FluentNHibernate.Search.Mapping.Parts
{
	public class BridgePart<T> where T : IHasBridge
    {
    	protected readonly T mappingPart;

		public BridgePart(T fieldMappingPart)
        {
            this.mappingPart = fieldMappingPart;
        }

        /// <summary>
        /// Sets a custom bridge type
        /// </summary>
        /// <typeparam name="TBridge"></typeparam>
        /// <param name="bridge"></param>
        /// <returns></returns>
		public T Custom<TBridge>(TBridge bridge) where TBridge : IFieldBridge
		{
			this.mappingPart.FieldBridge = bridge;
			return this.mappingPart;
		}

        /// <summary>
        /// Sets a custom bridge type. The type must have a parameterless constructor.
        /// </summary>
        /// <typeparam name="TBridge"></typeparam>
        /// <returns></returns>
		public T Custom<TBridge>() where TBridge : IFieldBridge, new()
		{
			return this.Custom(new TBridge());
		}

        /// <summary>
        /// Sets a Boolean Bridge
        /// </summary>
        /// <returns></returns>
		public T Boolean()
        {
		    return Custom(BridgeFactory.BOOLEAN);
        }

		public T DateDay()
        {
            return Custom(BridgeFactory.DATE_DAY);
        }

		public T DateHour()
        {
            return Custom(BridgeFactory.DATE_HOUR);
        }

		public T DateMillisecond()
        {
            return Custom(BridgeFactory.DATE_MILLISECOND);
        }

		public T DateMinute()
        {
            return Custom(BridgeFactory.DATE_MINUTE);
        }

		public T DateMonth()
        {
            return Custom(BridgeFactory.DATE_MONTH);
        }

		public T DateSecond()
        {
            return Custom(BridgeFactory.DATE_SECOND);
        }

		public T DateYear()
        {
            return Custom(BridgeFactory.DATE_YEAR);
        }

		public T Double()
        {
            return Custom(BridgeFactory.DOUBLE);
        }

		public T Float()
        {
            return Custom(BridgeFactory.FLOAT);
        }

		public T Integer()
        {
            return Custom(BridgeFactory.INTEGER);
        }

		public T Short()
        {
            return Custom(BridgeFactory.SHORT);
        }

		public T String()
        {
            return Custom(BridgeFactory.STRING);
        }

		public T Guid()
        {
            return Custom(BridgeFactory.GUID);
        }
    }
}