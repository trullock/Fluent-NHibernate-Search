using System;
using System.Linq.Expressions;
using System.Reflection;
using FluentNHibernate.Search.Exceptions;
using FluentNHibernate.Search.Mapping.Providers;
using FluentNHibernate.Search.Reflection;
using NHibernate.Properties;
using NHibernate.Search.Mapping;

namespace FluentNHibernate.Search.Mapping.Parts
{
    public class EmbeddedPart<T> : IEmbeddedMappingProvider
    {
    	private readonly Expression<Func<T, object>> property;
        public string prefix { get; protected set; }
    	private int depth;
    	private bool isCollection;
    	private Type targetElementType;

		public EmbeddedPart(Expression<Func<T, object>> property)
		{
			this.property = property;
		}

    	/// <summary>
        /// Defines the Embedded Prefix.
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public EmbeddedPart<T> Prefix(string prefix)
        {
            this.prefix = prefix;
            return this;
        }

        /// <summary>
        /// Defines the Embedded Depth
        /// Should not be used, not supported in current NHibernate.Search
        /// </summary>
        /// <param name="depth"></param>
        /// <returns></returns>
		[Obsolete("Should not be used, not supported in current NHibernate.Search")]
        public EmbeddedPart<T> Depth(int depth)
        {
        	this.depth = depth;
        	throw new NotSupportedException();
        }

    	/// <summary>
    	/// Defines the Embedded Target Type
    	/// Should not be used, not supported in current NHibernate.Search 
    	/// </summary>
    	/// <typeparam name="TTarget"></typeparam>
    	/// <returns></returns>
		[Obsolete("Should not be used, not supported in current NHibernate.Search")]
    	public EmbeddedPart<T> Target<TTarget>()
    	{
    		this.targetElementType = typeof (TTarget);
    		throw new NotSupportedException();
    	}

    	/// <summary>
    	/// Defines Embedded Field as Collection.
    	/// </summary>
    	/// <returns></returns>
    	public EmbeddedPart<T> AsCollection()
    	{
    		this.isCollection = true;
    		return this;
    	}

    	public void AssertIsValid()
    	{
    		if (this.property == null)
    			throw new EmbeddedMappingException("property cannot be null");
    	}

    	public EmbeddedMapping GetMapping()
    	{
    		var propertyInfo = this.property.ToPropertyInfo();
			var getter = new BasicPropertyAccessor.BasicGetter(typeof(T), propertyInfo, propertyInfo.Name);
    		var mapping = new EmbeddedMapping(new DocumentMapping(typeof(T)), getter)
    		              	{
    		              		Prefix = this.prefix,
    		              		IsCollection = this.isCollection
    		              	};
    		return mapping;
    	}
    }
}