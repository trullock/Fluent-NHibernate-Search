namespace FluentNHibernate.Search.Exceptions
{
    public class ContainedInMappingException : MappingException
    {
        public ContainedInMappingException(string message)
            : base(message)
        {
        }

        public ContainedInMappingException()
        {
        }
    }
}