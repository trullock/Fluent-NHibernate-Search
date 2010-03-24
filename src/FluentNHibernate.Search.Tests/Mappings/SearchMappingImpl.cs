using System.Reflection;
using FluentNHibernate.Search.Mapping;

namespace FluentNHibernate.Search.Tests.Mappings
{
    public class SearchMappingImpl : SearchMapping
    {
        protected override void Configure()
        {
            AddAssembly(Assembly.GetExecutingAssembly());
            AddAssemblyContaining<AuthorSearchMap>();

            AssertIsValid();
        }
    }
}