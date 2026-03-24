namespace Base.Factory.Instance
{
	public interface IInstanceFactory
	{
		T GetInstanceFromInterface<T>();
	}
}
