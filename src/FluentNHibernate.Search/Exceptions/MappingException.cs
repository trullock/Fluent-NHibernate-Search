using System;

namespace FluentNHibernate.Search.Exceptions
{
    public class MappingException : Exception
    {
        public MappingException(string message)
            : base(message)
        {
        }

        public MappingException()
        {
        }
    }
}