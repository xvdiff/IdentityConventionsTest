using System;
using System.Linq.Expressions;
using IdentityConventionsTest.Core;

namespace IdentityConventionsTest.Conventions
{
	public abstract class TypeIdentityConvention : IIdentityConvention
	{

		private readonly Func<Type, bool> _condition;
		private readonly Expression<Func<object, object>> _selector;

		protected TypeIdentityConvention(Func<Type, bool> condition, Expression<Func<object, object>> selector)
		{
			Guard.AgainstNullArgument(condition);
			Guard.AgainstNullArgument(selector);
			_condition = condition;
			_selector = selector;
		}
		
		public Expression<Func<TEntity, TIdentity>> ApplyOn<TEntity, TIdentity>()
		{
			var entityType = typeof (TEntity);

			if (!_condition(entityType)) 
				return null;

			var parameter = Expression.Parameter(entityType, "x");
			return Expression.Lambda<Func<TEntity, TIdentity>>(_selector.Body, parameter);
		}
	}
}
