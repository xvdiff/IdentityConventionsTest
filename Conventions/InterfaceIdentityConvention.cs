using System;
using System.Linq.Expressions;
using IdentityConventionsTest.Core;

namespace IdentityConventionsTest.Conventions
{
	public class InterfaceIdentityConvention<TInterface> : TypeIdentityConvention
	{

		public InterfaceIdentityConvention(Expression<Func<TInterface, object>> selector)
			: base(type => type.Implements<TInterface>(), Expression.Lambda<Func<object, object>>(
			Expression.Convert(selector.Body, typeof(object)), selector.Parameters))
		{
			
		}

	}
}
