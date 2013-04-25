using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using FluentNHibernate.Cfg;
using FluentNHibernate.Search.Cfg;
using FluentNHibernate.Search.Mapping;
using FluentNHibernate.Search.Tests.Domain;
using FluentNHibernate.Search.Tests.Mappings;
using NHibernate.Cfg;
using NHibernate.Properties;
using NHibernate.Search.Bridge;
using NHibernate.Search.Mapping;

namespace FluentNHibernate.Search.Tests.Integration
{
    public abstract class TestDocumentIntegrationSpecification : Integration_Specification<TestDocument>, ISearchMapping
    {
        protected override void fnhMappings(MappingConfiguration mapping)
        {
            mapping.FluentMappings.Add<TestDocumentMap>();
						mapping.FluentMappings.Add<TagDocumentMap>();
        }

        protected override Configuration searchConfig(FluentSearchConfiguration config)
        {
            return config
                .DefaultAnalyzer().Standard()
                .DirectoryProvider().RAMDirectory()
                .IndexingStrategy().Event()
                .MappingClass(GetType())
                .BuildConfiguration();
        }

        public ICollection<DocumentMapping> Build(Configuration cfg)
        {
            var mapping = this.GetMapping();

            return new List<DocumentMapping> { mapping };
        }

        protected override void Configure()
        {
            configureSearchMapping();
        }

        protected abstract void configureSearchMapping();
    }
}