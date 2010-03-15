using System;
using System.Collections.Generic;
using FluentNHibernate.Search.Mapping.Parts;
using NHibernate.Cfg;

namespace FluentNHibernate.Search.Cfg
{
    public interface IFluentSearchConfiguration
    {
        IDictionary<string, string> Properties { get; }
        Configuration BuildConfiguration();
    }
}