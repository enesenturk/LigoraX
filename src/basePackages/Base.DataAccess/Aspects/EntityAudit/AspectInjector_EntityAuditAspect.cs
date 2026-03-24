namespace Base.DataAccess.Aspects.EntityAudit
{
	//[Aspect(Scope.Global)]
	//[Injection(typeof(EntityAuditAspect))]
	//public class EntityAuditAspect : Attribute
	//{

	//	[Advice(Kind.Before, Targets = Target.Method)]
	//	public void OnEntry(
	//		[Argument(Source.Metadata)] MethodBase method,
	//		[Argument(Source.Arguments)] object[] args
	//		)
	//	{
	//		CrudOperationType operationType = method.GetCustomAttribute<EntityAuditAspectOptions>().OperationType;

	//		if (operationType is CrudOperationType.Read)
	//			return;

	//		if (args.Length is 0)
	//			throw new AbsurdOperationException("audit operation arguments must contain entity and executingUserId.");

	//		int executingUserId = EntityAuditAspectHelper.GetExecutingUserId(args);

	//		bool isBulkOperation = EntityAuditAspectHelper.IsArgGenericList(args[0]);

	//		if (isBulkOperation)
	//		{
	//			if (!EntityAuditAspectHelper.IsListOfIEntity(args[0], out List<IMutationEntity> entities))
	//				throw new AbsurdOperationException("bulk audit operation must contain List<IEntity> as first argument.");

	//			if (entities.Count is 0)
	//				throw new AbsurdOperationException("bulk audit operation must contain entities.");

	//			EntityAuditAspectHelper.SetAuditFields(ref entities, executingUserId, operationType);
	//		}
	//		else
	//		{
	//			if (!EntityAuditAspectHelper.IsIEntity(args[0], out IMutationEntity entity))
	//				throw new AbsurdOperationException("audit operation must contain IEntity as first argument.");

	//			EntityAuditAspectHelper.SetAuditFields(ref entity, executingUserId, operationType);
	//		}
	//	}

	//}
}
