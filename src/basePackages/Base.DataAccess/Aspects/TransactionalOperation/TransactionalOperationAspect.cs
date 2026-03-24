using AspectCore.DynamicProxy;
using System.Transactions;

namespace Base.DataAccess.Aspects.TransactionalOperation
{

	[AttributeUsage(AttributeTargets.Method)]
	public class TransactionalOperationAspect : AbstractInterceptorAttribute
	{

		public override async Task Invoke(AspectContext context, AspectDelegate next)
		{
			using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
			{
				await next(context);

				scope.Complete();
			}
		}

	}
}
