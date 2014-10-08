using System;
using System.Linq.Expressions;

namespace IdentityConventionsTest
{
	public interface IEntityStoreConfiguration<TEntity, TIdentity>
		where TEntity : class
	{

		Expression<Func<TEntity, TIdentity>> IdentitySelector { get; set; } 

	}
}
