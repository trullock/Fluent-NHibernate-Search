namespace FluentNHibernate.Search.Exceptions
{
    public class DocumentMappingException : MappingException
    {
        public DocumentMappingException(string message)
            : base(message)
        {
        }

        public DocumentMappingException()
        {
        }
    }
}