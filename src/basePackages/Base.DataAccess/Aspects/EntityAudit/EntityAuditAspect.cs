using AspectCore.DynamicProxy;
using Base.DataAccess.Models;
using Base.Entity;
using Base.Exceptions.ExceptionModels;
using System.Reflection;

namespace Base.DataAccess.Aspects.EntityAudit
{
	[AttributeUsage(AttributeTargets.Method)]
	public class EntityAuditAspect : AbstractInterceptorAttribute
	{

		public CrudOperationType OperationType { get; set; }

		public override async Task Invoke(AspectContext context, AspectDelegate next)
		{
			MethodInfo method = context.ServiceMethod;
			object[] args = context.Parameters;

			CrudOperationType operationType = OperationType;

			if (operationType is CrudOperationType.Read)
			{
				await next(context);
				return;
			}

			if (args.Length is 0)
				throw new AbsurdOperationException("audit operation arguments must contain entity and executingUserId.");

			Guid executingUserId = EntityAuditAspectHelper.GetExecutingUserId(args);

			bool isBulkOperation = EntityAuditAspectHelper.IsArgGenericList(args[0]);

			if (isBulkOperation)
			{
				if (!EntityAuditAspectHelper.IsListOfIEntity(args[0], out List<IMutationEntity> entities))
					throw new AbsurdOperationException("bulk audit operation must contain List<IEntity> as first argument.");

				if (entities.Count is 0)
					throw new AbsurdOperationException("bulk audit operation must contain entities.");

				EntityAuditAspectHelper.SetAuditFields(ref entities, executingUserId, operationType);
			}
			else
			{
				if (!EntityAuditAspectHelper.IsIEntity(args[0], out IMutationEntity entity))
					throw new AbsurdOperationException("audit operation must contain IEntity as first argument.");

				EntityAuditAspectHelper.SetAuditFields(ref entity, executingUserId, operationType);
			}

			await next(context);
		}
	}
}
