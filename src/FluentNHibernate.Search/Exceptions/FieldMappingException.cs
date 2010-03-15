namespace FluentNHibernate.Search.Exceptions
{
    public class FieldMappingException : MappingException
    {
        public FieldMappingException(string message)
            : base(message)
        {
        }

        public FieldMappingException()
        {
        }
    }
}