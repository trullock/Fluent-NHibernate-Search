using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentNHibernate.Search.Mapping.Providers;
using NHibernate.Cfg;
using NHibernate.Search.Mapping;

namespace FluentNHibernate.Search.Mapping
{
    public abstract class SearchMapping : ISearchMapping
    {
    	private readonly IList<Assembly> assemblies;
    	private readonly IList<Type> explicitProviders;
    	private bool assert;

        protected SearchMapping()
        {
            this.assemblies = new List<Assembly>();
			this.explicitProviders = new List<Type>();
        }

        public ICollection<DocumentMapping> Build(Configuration cfg)
        {
            Configure();

            var providers = GetMappingProviders();

            if (this.assert)
            {
            	foreach (var provider in providers)
            		provider.AssertIsValid();
            }

            return providers.Select(o => o.GetMapping()).ToList();
        }

        protected abstract void Configure();

        private IEnumerable<IDocumentMappingProvider> GetMappingProviders()
        {
            var mappingProviderType = typeof(IDocumentMappingProvider);

            var types = this.assemblies
				.SelectMany(a => a.GetTypes())
				.Where(t => !t.IsGenericType && !t.IsAbstract && mappingProviderType.IsAssignableFrom(t));

			foreach (var type in types)
				yield return Activator.CreateInstance(type) as IDocumentMappingProvider;

			foreach (var type in explicitProviders)
				yield return Activator.CreateInstance(type) as IDocumentMappingProvider;
        }

        protected void AddAssemblyContaining<T>()
        {
            var type = typeof (T);
            var asm = Assembly.GetAssembly(type);

            if (asm == null)
            {
                throw new InvalidOperationException(
                    String.Format("The assembly with the specified type '{0}' doesn't exists", type.Name));
            }

            AddAssembly(asm);
        }

		protected void Add<TDocumentMappingProvider>() where TDocumentMappingProvider : IDocumentMappingProvider
		{
			this.explicitProviders.Add(typeof(TDocumentMappingProvider));
		}

        protected void AddAssembly(Assembly asm)
        {
            if (!this.assemblies.Contains(asm))
            {
                this.assemblies.Add(asm);
            }
        }

        protected void AssertIsValid()
        {
            this.assert = true;
        }
    }
}