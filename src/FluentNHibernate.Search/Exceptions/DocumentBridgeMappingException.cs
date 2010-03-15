namespace FluentNHibernate.Search.Exceptions
{
    public class DocumentBridgeMappingException : MappingException
    {
        public DocumentBridgeMappingException(string message)
            : base(message)
        {
        }

        public DocumentBridgeMappingException()
        {
        }
    }
}