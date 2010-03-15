using System;
using System.Collections.Generic;
using FluentNHibernate.Search.Mapping;
using NHibernate.Cfg;
using NHibernate.Search.Mapping;

namespace FluentNHibernate.Search.Tests.Mappings
{
    public class TestDocumentIdAndStringSearchMapping : DocumentMap<TestDocument>, ISearchMapping
    {
        public ICollection<DocumentMapping> Build(Configuration cfg)
        {
            return new List<DocumentMapping> {this.GetMapping()};
        }

        protected override void Configure()
        {
            Id(d => d.Id);
            Map(d => d.String);
        }
    }
}