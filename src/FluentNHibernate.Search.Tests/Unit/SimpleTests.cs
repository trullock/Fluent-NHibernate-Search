using System.Reflection;
using FluentNHibernate.Search.Tests.Mappings;
using NUnit.Framework;

namespace FluentNHibernate.Search.Tests.Unit
{
    [TestFixture]
    public class SimpleTests
    {
        [Test]
        public void Mapping_Should_Initialize()
        {
            var builder = new Mappings.SearchMappingImpl();
            builder.Build(null);
        }
    }
}