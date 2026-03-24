namespace Base.DataAccess.Aspects.TransactionalOperation
{
	//[Aspect(Scope.Global)]
	//[Injection(typeof(TransactionalOperationAspect))]
	//public class TransactionalOperationAspect : Attribute
	//{

	//	[Advice(Kind.Around, Targets = Target.Method)]
	//	public object OnAround(
	//		[Argument(Source.Target)] Func<object[], object> method,
	//		[Argument(Source.ReturnType)] Type returnType,
	//		[Argument(Source.Arguments)] object[] args
	//	)
	//	{
	//		TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
	//		//TransactionScope scope = new TransactionScope();

	//		try
	//		{
	//			object result = method(args);

	//			if (result is Task task)
	//				task.GetAwaiter().GetResult();

	//			scope.Complete();
	//			scope.Dispose();

	//			return result;
	//		}
	//		catch
	//		{
	//			scope.Dispose();

	//			throw;
	//		}
	//	}

	//}
}
