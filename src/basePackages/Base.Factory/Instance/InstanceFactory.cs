namespace Base.Factory.Instance
{
	public class InstanceFactory : IInstanceFactory
	{

		#region CTOR

		private IServiceProvider _serviceProvider;

		public InstanceFactory(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		#endregion

		public T GetInstanceFromInterface<T>()
		{
			return (T)_serviceProvider.GetService(typeof(T));
		}

	}
}
