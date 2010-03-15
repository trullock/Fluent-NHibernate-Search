using System.Reflection;
using FluentNHibernate.Search.Exceptions;
using FluentNHibernate.Search.Mapping.Providers;
using NHibernate.Search.Bridge;
using NHibernate.Search.Mapping;

namespace FluentNHibernate.Search.Mapping.Parts
{
    public class DocumentIdMappingPart : FluentMappingPart, IDocumentIdMappingProvider
    {
        public DocumentIdMappingPart(PropertyInfo propertyInfo) : base(propertyInfo)
        {
        }

        /// <summary>
        /// Defines the DocumentId Field Bridge.
        /// </summary>
        /// <returns></returns>
        public DocumentIdBridgePart<DocumentIdMappingPart> Bridge()
        {
            return new DocumentIdBridgePart<DocumentIdMappingPart>(this);
        }

        public DocumentIdMapping GetMapping()
        {
            var mappingPart = this as IFluentMappingPart;

            var mapping = new DocumentIdMapping(mappingPart.Name, (this as IHasBridge).FieldBridge as ITwoWayFieldBridge, this.Getter)
                              {
                                  Boost = mappingPart.Boost
                              };
            return mapping;
        }

        public void AssertIsValid()
        {
            var mappingPart = this as IFluentMappingPart;

            if (string.IsNullOrEmpty(mappingPart.Name))
                throw new DocumentIdMappingException("Field name cannot be null");

            if ((this as IHasBridge).FieldBridge == null || !typeof (ITwoWayFieldBridge).IsAssignableFrom((this as IHasBridge).FieldBridge.GetType()))
                throw new DocumentIdMappingException("Bridge cannot be an ITwoWayFieldBridge");
        }
    }
}