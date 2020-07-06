using System;
using System.Linq.Expressions;

namespace Fluent.Testing.Library.Infrastructure
{
    internal static class PropertyExpressionHelper
    {
        public static string GetPropertyName<T>(Expression<Func<T, object?>> expression)
        {
            var member = expression.Body as MemberExpression ?? ((expression.Body as UnaryExpression)?.Operand as MemberExpression);

            if (member == null)
            {
                throw new ArgumentException("Action must be a member expression.");
            }

            return member.Member.Name;
        }
    }
}