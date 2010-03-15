using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace FluentNHibernate.Search.Tests.Extensions
{
	public static class ShouldExtensions
	{
		public static void ShouldBeEmpty(this string aString)
		{
			Assert.IsEmpty(aString);
		}

		public static string ShouldBeEqualIgnoringCase(this string actual, string expected)
		{
			Assert.AreEqual(expected.ToLowerInvariant(), actual.ToLowerInvariant());
			return expected;
		}

		public static void ShouldBeFalse(this bool condition)
		{
			Assert.False(condition);
		}

		public static IComparable ShouldBeGreaterThan(this IComparable arg1, IComparable arg2)
		{
			Assert.AreEqual(1, arg1.CompareTo(arg2));
			return arg2;
		}

		public static IComparable ShouldBeLessThan(this IComparable arg1, IComparable arg2)
		{
			Assert.AreEqual(-1, arg1.CompareTo(arg2));
			return arg2;
		}

		public static void ShouldBeNull(this object anObject)
		{
			Assert.Null(anObject);
		}

		public static void ShouldBeOfType<T>(this object actual)
		{
			Assert.IsInstanceOf<T>(actual);
		}

		public static void ShouldBeOfType(this object actual, Type expected)
		{
			Assert.IsInstanceOf(expected, actual);
		}

		public static void ShouldBeSurroundedWith(this string actual, string expectedDelimiter)
		{
			Assert.True(actual.StartsWith(expectedDelimiter));
			Assert.True(actual.EndsWith(expectedDelimiter));
		}

		public static void ShouldBeSurroundedWith(this string actual, string expectedStartDelimiter, string expectedEndDelimiter)
		{
			Assert.True(actual.StartsWith(expectedStartDelimiter));
			Assert.True(actual.EndsWith(expectedEndDelimiter));
		}

		public static object ShouldBeTheSameAs(this object actual, object expected)
		{
			Assert.AreSame(expected, actual);
			return expected;
		}

		public static void ShouldBeTrue(this bool condition)
		{
			Assert.True(condition);
		}

		public static void ShouldContain(this IList actual, params object[] expected)
		{
			foreach (object item in expected)
				Assert.Contains(item, actual);
		}

		public static void ShouldContain(this string actual, string expected)
		{
			Assert.True(actual.Contains(expected));
		}

		public static void ShouldContainErrorMessage(this Exception exception, string expected)
		{
			Assert.True(exception.Message.Contains(expected));
		}

		public static void ShouldContainOnly<T>(this IEnumerable<T> actual, params T[] expected)
		{
			actual.ShouldContainOnly(new List<T>(expected));
		}

		public static void ShouldContainOnly<T>(this IEnumerable<T> actual, IEnumerable<T> expected)
		{
			var actualList = new List<T>(actual);
			var remainingList = new List<T>(actualList);
			foreach (T item in expected)
			{
				Assert.Contains(item, actualList);
				remainingList.Remove(item);
			}
			Assert.IsEmpty(remainingList);
		}

		public static void ShouldEndWith(this string actual, string expected)
		{
			Assert.True(actual.EndsWith(expected));
		}

		public static void ShouldEqual<T>(this T actual, T expected)
		{
			Assert.AreEqual(expected, actual);
		}

		public static void ShouldNotBeEmpty(this string aString)
		{
			Assert.IsNotEmpty(aString);
		}

		public static void ShouldNotBeNull(this object anObject)
		{
			Assert.NotNull(anObject);
		}

		public static void ShouldNotBeOfType(this object actual, Type expected)
		{
			Assert.IsNotInstanceOf(expected, actual);
		}

		public static void ShouldNotBeTheSameAs(this object actual, object expected)
		{
			Assert.AreNotSame(expected, actual);
		}

		public static void ShouldNotContain(this IEnumerable collection, object expected)
		{
			int i = 0;
			foreach(var item in collection)
			{
				if(item.Equals(expected))
					Assert.Fail("Collection DOES contain item at position " + i);
				i++;
			}
		}

		public static void ShouldNotEqual<T>(this T actual, T expected)
		{
			Assert.AreNotEqual(expected, actual);
		}

		public static void ShouldStartWith(this string actual, string expected)
		{
			Assert.True(actual.StartsWith(expected));
		}

		public static void ShouldBeNullOrEmpty(this string actual)
		{
			Assert.True(string.IsNullOrEmpty(actual), "Expected null or empty, got: " + actual);
		}
	}
}