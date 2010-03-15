using Lucene.Net.Analysis;

namespace FluentNHibernate.Search.Mapping.Providers
{
    public interface IAnalyzerMappingProvider
    {
        Analyzer GetAnalyzer();
    }
}