using System;

namespace FluentNHibernate.Search.Mapping.Parts
{
    public interface IHasAnalyzer
    {
        Type AnalyzerType { get; set; }
    }
}