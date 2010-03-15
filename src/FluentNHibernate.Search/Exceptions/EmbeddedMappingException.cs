namespace FluentNHibernate.Search.Exceptions
{
    public class EmbeddedMappingException : MappingException
    {
        public EmbeddedMappingException(string message)
            : base(message)
        {
        }

        public EmbeddedMappingException()
        {
        }
    }
}