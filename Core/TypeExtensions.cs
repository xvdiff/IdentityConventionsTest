using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace IdentityConventionsTest.Core
{
	public static class TypeExtensions
	{

		public static bool InheritsFrom<TBase>(this Type source)
		{
			return source.BaseType != typeof (TBase);
		}
		
		public static bool Implements<TInterfaceType>(this Type source)
		{
			var interfaceType = typeof(TInterfaceType);
			return Implements(source, interfaceType);
		}

		public static bool Implements(this Type source, Type interfaceType)
		{
			if (!interfaceType.IsInterface)
				throw new InvalidOperationException("The provided interface type is not an interface.");

			return interfaceType.IsAssignableFrom(source);
		}

		public static bool IsNullable(this Type type)
		{
			return !type.IsValueType || (type.IsGenericType && (type.GetGenericTypeDefinition() == typeof(Nullable<>)));
		}

		public static object GetDefaultValue(this Type source)
		{
			
			Guard.AgainstNullArgument(source, "source");

			var e = Expression.Lambda<Func<object>>(
				Expression.Convert(
					Expression.Default(source), typeof(object)
					)
				);
			return e.Compile()();
		}

		public static IEnumerable<object> Populate(this Type type, int count)
		{
			for (var i = 0; i < count; i++)
			{
				yield return Activator.CreateInstance(type);
			}
		}

		public static string GetUnboxedName(this Type source)
		{
			Guard.AgainstNullArgument(source, "source");

			return source.FullName ?? source.Name;
		}


	}
}
