namespace FluentNHibernate.Search.Exceptions
{
    public class DocumentIdMappingException : MappingException
    {
        public DocumentIdMappingException(string message)
            : base(message)
        {
        }

        public DocumentIdMappingException()
        {
        }
    }
}