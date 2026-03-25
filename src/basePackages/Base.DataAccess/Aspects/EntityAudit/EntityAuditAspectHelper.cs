using Base.DataAccess.Models;
using Base.Entity;
using Base.Exceptions.ExceptionModels;
using Base.PrimitiveTypeHelpers._DateTime.Entensions;
using System.Collections;

namespace Base.DataAccess.Aspects.EntityAudit
{
	public class EntityAuditAspectHelper
	{

		internal static Guid GetExecutingUserId(object[] args)
		{
			foreach (var arg in args)
			{
				if (arg is Guid executingUserId)
				{
					if (executingUserId == Guid.Empty)
						throw new AbsurdOperationException("audit operation executingUserId is required and cannot be empty Guid");

					return executingUserId;
				}
			}

			throw new AbsurdOperationException("audit operation executingUserId is required and cannot be empty Guid");
		}

		internal static bool IsArgGenericList(object arg)
		{
			var type = arg.GetType();

			if (type.IsGenericType)
			{
				var genericTypeDefinition = type.GetGenericTypeDefinition();

				if (genericTypeDefinition == typeof(List<>))
				{
					return true;
				}
			}

			return false;
		}

		internal static bool IsIEntity(object arg, out IMutationEntity entity)
		{
			entity = null;

			if (arg == null)
				return false;

			var type = arg.GetType();

			while (type != null)
			{
				if (typeof(IMutationEntity).IsAssignableFrom(type))
				{
					entity = arg as IMutationEntity;
					return true;
				}

				type = type.BaseType;
			}

			return false;
		}

		internal static bool IsListOfIEntity(object arg, out List<IMutationEntity> entities)
		{
			entities = null;

			if (arg == null)
				return false;

			var genericArgumentType = arg.GetType().GetGenericArguments()[0];

			if (typeof(IMutationEntity).IsAssignableFrom(genericArgumentType))
			{
				entities = new List<IMutationEntity>();

				foreach (var item in (IEnumerable)arg)
				{
					if (item is IMutationEntity entity)
					{
						entities.Add(entity);
					}
					else
					{
						return false;
					}
				}

				return true;
			}

			return false;
		}

		internal static void SetAuditFields(ref IMutationEntity entity, Guid executingUserId, CrudOperationType operationType)
		{
			if (operationType is CrudOperationType.Create)
			{
				entity.created_at = DateTime.Now.ToUniversalTimeZone();
				entity.created_by = executingUserId;
			}
			else if (operationType is CrudOperationType.Update || operationType is CrudOperationType.Delete)
			{
				entity.updated_at = DateTime.Now.ToUniversalTimeZone();
				entity.updated_by = executingUserId;
			}
		}

		internal static void SetAuditFields(ref List<IMutationEntity> entities, Guid executingUserId, CrudOperationType operationType)
		{
			entities.ForEach(entity =>
			{
				SetAuditFields(ref entity, executingUserId, operationType);
			});
		}

	}
}
