namespace IdentityConventionsTest
{
	public interface IGenericFactory<out TProduct>
	{

		TProduct Create();

	}
}
