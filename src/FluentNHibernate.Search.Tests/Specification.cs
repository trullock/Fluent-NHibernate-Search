using NUnit.Framework;

namespace FluentNHibernate.Search.Tests
{
	public abstract class Specification
	{
		[SetUp]
		public void SetUp()
		{
			Given();
			When();
		}


		public virtual void Given()
		{
			
		}

		protected abstract void When();


		[TearDown]
		public virtual void TidyUp()
		{
			
		}
	}
}