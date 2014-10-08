using System;
using System.Linq.Expressions;

namespace IdentityConventionsTest.Conventions
{
	public interface IIdentityConvention
	{

		Expression<Func<TEntity, TIdentiy>> ApplyOn<TEntity, TIdentiy>();

	}
}
