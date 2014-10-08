using System.Collections.Generic;
using System.Linq;
using IdentityConventionsTest.Conventions;
using IdentityConventionsTest.Core;

namespace IdentityConventionsTest
{
	public abstract class EntityStoreConfigurationFactoryBase<TEntity, TIdentity, TConfiguration> : 
		IEntityStoreConfigurationFactory<TEntity, TIdentity, TConfiguration>
		where TConfiguration : IEntityStoreConfiguration<TEntity, TIdentity>
		where TEntity : class 
	{
		
		private readonly List<IIdentityConvention> _identityConventions = new List<IIdentityConvention>(); 
		
		public List<IIdentityConvention> IdentityConventions
		{
			get { return _identityConventions; }
		}

		public TConfiguration Create()
		{
			var configuration = FastActivator.Create<TConfiguration>();
			configuration.IdentitySelector =
				IdentityConventions.Where(x => x != null).Select(x => x.ApplyOn<TEntity, TIdentity>()).Single();

			return configuration;
		}
	}
}
