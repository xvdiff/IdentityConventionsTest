using System;
using IdentityConventionsTest.Core;

namespace IdentityConventionsTest.Conventions
{
	public class AttributeIdentityConvention<TAttribute> : PropertyIdentityConventionBase
		where TAttribute : Attribute
	{

		public AttributeIdentityConvention()
			: base(p => p.HasAttribute<TAttribute>())
		{
			
		}

	}
}
