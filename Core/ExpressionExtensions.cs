using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace IdentityConventionsTest.Core
{
	public static class ExpressionExtensions
	{
		public static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second,
			Func<Expression, Expression, Expression> merge)
		{
			return Expression.Lambda<T>(merge(first.Body, second.ReplaceParameters(first)), first.Parameters);
		}

		public static LambdaExpression ReplaceParameters(this LambdaExpression first, LambdaExpression second)
		{
			var map =
				first.Parameters.Select((f, i) => new { f, s = second.Parameters[i] }).ToDictionary(p => p.s, p => p.f);
			// replace parameters in the second lambda expression with parameters from the first
			var secondBody = ParameterRebinderVisitor.ReplaceParameters(map, second.Body);
			return Expression.Lambda(secondBody, first.Parameters);
		}

		public static Expression<Func<TFirstParam, TResult>> Compose<TFirstParam, TIntermediate, TResult>(
			this Expression<Func<TFirstParam, TIntermediate>> first,
			Expression<Func<TIntermediate, TResult>> second)
		{
			return Expression.Lambda<Func<TFirstParam, TResult>>(second.ReplaceParameters(first), first.Parameters);
		}

		public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first,
			Expression<Func<T, bool>> second)
		{
			return first.Compose(second, Expression.And);
		}

		public static Expression<Func<T, bool>> AndAlso<T>(this Expression<Func<T, bool>> first,
			Expression<Func<T, bool>> second)
		{
			return first.Compose(second, Expression.AndAlso);
		}

		public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first,
			Expression<Func<T, bool>> second)
		{
			return first.Compose(second, Expression.Or);
		}

		public static Expression<Func<T, bool>> OrElse<T>(this Expression<Func<T, bool>> first,
			Expression<Func<T, bool>> second)
		{
			return first.Compose(second, Expression.OrElse);
		}

		public static Expression<Func<T, bool>> AndNot<T>(this Expression<Func<T, bool>> first,
			Expression<Func<T, bool>> second)
		{
			return first.Compose(Not(second), Expression.And);
		}

		public static Expression<Func<T, bool>> OrNot<T>(this Expression<Func<T, bool>> first,
			Expression<Func<T, bool>> second)
		{
			return first.Compose(second.Not(), Expression.Or);
		}

		public static Expression<Func<T, bool>> ExclusiveOr<T>(this Expression<Func<T, bool>> first,
			Expression<Func<T, bool>> second)
		{
			return first.Compose(second, Expression.ExclusiveOr);
		}

		public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> expression)
		{
			var body = Expression.Not(expression.Body);
			return Expression.Lambda<Func<T, bool>>(body, expression.Parameters);
		}

		public static MemberInfo GetMember<T, TProperty>(this Expression<Func<T, TProperty>> expression)
		{
			var memberExp = RemoveUnary(expression.Body);
			return memberExp == null ? null : memberExp.Member;
		}

		private static MemberExpression RemoveUnary(Expression toUnwrap)
		{
			var unwrap = toUnwrap as UnaryExpression;
			if (unwrap != null)
			{
				return unwrap.Operand as MemberExpression;
			}

			return toUnwrap as MemberExpression;
		}
	}
}
