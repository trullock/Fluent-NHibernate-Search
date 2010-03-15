using System;
using System.Linq.Expressions;
using System.Reflection;

namespace FluentNHibernate.Search.Reflection
{
    public static class ReflectionExtensions
    {
        public static PropertyInfo ToPropertyInfo<T, TResult>(this Expression<Func<T, TResult>> expression)
        {
            MemberExpression memberExpression;
            var unary = expression.Body as UnaryExpression;
            if (unary != null)
            {
                memberExpression = unary.Operand as MemberExpression;
            }
            else
            {
                memberExpression = expression.Body as MemberExpression;
            }
            if (memberExpression == null || !(memberExpression.Member is PropertyInfo))
            {
                throw new ArgumentException("Expected property expression");
            }
            return (PropertyInfo)memberExpression.Member;
        }

        public static Type GetMemberTypeOrGenericArguments(this MemberInfo member)
        {
            var type = GetMemberType(member);
            if (type.IsGenericType)
            {
                var arguments = type.GetGenericArguments();
                return arguments[arguments.Length - 1];
            }

            return type;
        }

        public static Type GetMemberTypeOrGenericCollectionType(this MemberInfo member)
        {
            var type = GetMemberType(member);
            return type.IsGenericType ? type.GetGenericTypeDefinition() : type;
        }

        public static Type GetMemberType(this MemberInfo member)
        {
            var info = member as PropertyInfo;
            return info != null ? info.PropertyType : ((FieldInfo)member).FieldType;
        }
    }
}