using System.Reflection;
using FluentNHibernate.Search.Mapping;

namespace FluentNHibernate.Search.Tests.Mappings
{
    public class SearchMappingImpl : SearchMapping
    {
        #region Overrides of SearchMappingImpl

        protected override void Configure()
        {
            AddAssembly(Assembly.GetExecutingAssembly());
            AddAssemblyContaining<AuthorSearchMap>();

            AssertIsValid();
        }

        #endregion
    }
}