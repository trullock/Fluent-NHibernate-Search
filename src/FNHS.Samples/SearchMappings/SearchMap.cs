using System.Reflection;
using FluentNHibernate.Search.Mapping;

namespace FNHS.Samples.SearchMappings
{
    public class SearchMap : SearchMapping
    {
        protected override void Configure()
        {
            AddAssembly(Assembly.GetExecutingAssembly());
        }
    }
}