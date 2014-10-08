using System.Collections.Generic;
using IdentityConventionsTest.Conventions;

namespace IdentityConventionsTest
{
	public interface IEntityStoreConfigurationFactory<TEntity, TIdentity, out TConfiguration> : IGenericFactory<TConfiguration>
		where TConfiguration : IEntityStoreConfiguration<TEntity, TIdentity>
		where TEntity : class
	{

		List<IIdentityConvention> IdentityConventions { get; }

	}
}
