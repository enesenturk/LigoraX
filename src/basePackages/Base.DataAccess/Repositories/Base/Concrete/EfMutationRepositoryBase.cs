using Base.DataAccess.Aspects.EntityAudit;
using Base.DataAccess.Models;
using Base.DataAccess.Repositories.Base.Abstract;
using Base.Entity;
using Base.Exceptions.ExceptionModels;
using Microsoft.EntityFrameworkCore;

namespace Base.DataAccess.Repositories.Base.Concrete
{
	public class EfMutationRepositoryBase<T, TContext> : EfReadRepositoryBase<T, TContext>, IAsyncMutationRepositoryBase<T>, IMutationRepositoryBase<T>
		where T : IMutationEntity
		where TContext : DbContext, new()
	{

		#region Create

		#region async

		[EntityAuditAspect(OperationType = CrudOperationType.Create)]
		public async Task<T> AddAsync(T entity, int executingUserId)
		{
			using (var context = new TContext())
			{
				context.Entry(entity).State = EntityState.Added;

				await context.SaveChangesAsync();

				return entity;
			}
		}

		[EntityAuditAspect(OperationType = CrudOperationType.Create)]
		public async Task AddRangeAsync(List<T> entities, int executingUserId)
		{
			int takeCount = 999;

			int counter = entities.Count % takeCount == 0
				? entities.Count / takeCount
				: entities.Count / takeCount + 1;

			for (int i = 0; i < counter; i++)
			{
				using (var context = new TContext())
				{
					try
					{
						List<T> subList = entities.Skip(i * takeCount).Take(takeCount).ToList();

						context.ChangeTracker.AutoDetectChangesEnabled = false;

						await context.Set<T>().AddRangeAsync(subList);
						await context.SaveChangesAsync();
					}
					finally
					{
						context.ChangeTracker.AutoDetectChangesEnabled = true;
					}
				}
			}
		}

		#endregion

		#region sync

		[EntityAuditAspect(OperationType = CrudOperationType.Create)]
		public T Add(T entity, int executingUserId)
		{
			using (var context = new TContext())
			{
				context.Entry(entity).State = EntityState.Added;
				context.SaveChanges();
				return entity;
			}
		}

		[EntityAuditAspect(OperationType = CrudOperationType.Create)]
		public void AddRange(List<T> entities, int executingUserId)
		{
			int takeCount = 999;

			int counter = entities.Count % takeCount == 0
				? entities.Count / takeCount
				: entities.Count / takeCount + 1;

			for (int i = 0; i < counter; i++)
			{
				using (var context = new TContext())
				{
					try
					{
						List<T> subList = entities.Skip(i * takeCount).Take(takeCount).ToList();

						context.ChangeTracker.AutoDetectChangesEnabled = false;

						context.Set<T>().AddRange(subList);
						context.SaveChanges();
					}
					finally
					{
						context.ChangeTracker.AutoDetectChangesEnabled = true;
					}
				}
			}
		}

		#endregion

		#endregion

		#region Update

		#region async

		[EntityAuditAspect(OperationType = CrudOperationType.Update)]
		public async Task<T> UpdateAsync(T entity, int executingUserId, params string[] fields)
		{
			using (var context = new TContext())
			{
				if (fields.Length > 0)
				{
					context.Entry(entity).Property(nameof(IMutationEntity.update_user)).IsModified = true;
					context.Entry(entity).Property(nameof(IMutationEntity.update_date)).IsModified = true;

					foreach (string field in fields)
						context.Entry(entity).Property(field).IsModified = true;
				}
				else
				{
					context.Entry(entity).State = EntityState.Modified;
				}

				await context.SaveChangesAsync();

				return entity;
			}
		}

		[EntityAuditAspect(OperationType = CrudOperationType.Update)]
		public async Task UpdateRangeAsync(List<T> entities, int executingUserId, params string[] fields)
		{
			int takeCount = 999;

			int counter = entities.Count % takeCount == 0
				? entities.Count / takeCount
				: entities.Count / takeCount + 1;

			for (int i = 0; i < counter; i++)
			{
				using (var context = new TContext())
				{
					try
					{
						List<T> subList = entities.Skip(i * takeCount).Take(takeCount).ToList();

						context.ChangeTracker.AutoDetectChangesEnabled = false;

						foreach (T entity in subList)
						{
							var updatedEntity = context.Entry(entity);

							if (fields.Length > 0)
							{
								updatedEntity.Property(nameof(IMutationEntity.update_user)).IsModified = true;
								updatedEntity.Property(nameof(IMutationEntity.update_date)).IsModified = true;

								foreach (string field in fields)
									context.Entry(entity).Property(field).IsModified = true;
							}
							else
							{
								updatedEntity.State = EntityState.Modified;
							}
						}

						await context.SaveChangesAsync();
					}
					finally
					{
						context.ChangeTracker.AutoDetectChangesEnabled = true;
					}
				}
			}
		}

		#endregion

		#region sync

		[EntityAuditAspect(OperationType = CrudOperationType.Update)]
		public T Update(T entity, int executingUserId, params string[] fields)
		{
			using (var context = new TContext())
			{
				if (fields.Length > 0)
				{
					context.Entry(entity).Property(nameof(IMutationEntity.update_user)).IsModified = true;
					context.Entry(entity).Property(nameof(IMutationEntity.update_date)).IsModified = true;

					foreach (string field in fields)
						context.Entry(entity).Property(field).IsModified = true;
				}
				else
				{
					context.Entry(entity).State = EntityState.Modified;
				}

				context.SaveChanges();

				return entity;
			}
		}

		[EntityAuditAspect(OperationType = CrudOperationType.Update)]
		public void UpdateRange(List<T> entities, int executingUserId, params string[] fields)
		{
			int takeCount = 999;

			int counter = entities.Count % takeCount == 0
				? entities.Count / takeCount
				: entities.Count / takeCount + 1;

			for (int i = 0; i < counter; i++)
			{
				using (var context = new TContext())
				{
					try
					{
						context.ChangeTracker.AutoDetectChangesEnabled = false;

						foreach (T entity in entities)
						{
							var updatedEntity = context.Entry(entity);

							if (fields.Length > 0)
							{
								updatedEntity.Property(nameof(IMutationEntity.update_user)).IsModified = true;
								updatedEntity.Property(nameof(IMutationEntity.update_date)).IsModified = true;

								foreach (string field in fields)
									context.Entry(entity).Property(field).IsModified = true;
							}
							else
							{
								updatedEntity.State = EntityState.Modified;
							}
						}

						context.SaveChanges();
					}
					finally
					{
						context.ChangeTracker.AutoDetectChangesEnabled = true;
					}
				}
			}
		}

		#endregion

		#endregion

		#region Delete

		#region async

		[EntityAuditAspect(OperationType = CrudOperationType.Delete)]
		public async Task HardDeleteAsync(T entity, int executingUserId)
		{
			using (var context = new TContext())
			{
				if (entity is null)
					throw new NotFoundException();

				var deletedEntity = context.Entry(entity);

				deletedEntity.State = EntityState.Deleted;

				await context.SaveChangesAsync();
			}
		}

		[EntityAuditAspect(OperationType = CrudOperationType.Delete)]
		public async Task SoftDeleteAsync(T entity, int executingUserId)
		{
			using (var context = new TContext())
			{
				if (entity is null)
					throw new NotFoundException();

				entity.is_deleted = true;

				await UpdateAsync(entity, executingUserId, [nameof(IMutationEntity.is_deleted)]);
			}
		}

		[EntityAuditAspect(OperationType = CrudOperationType.Delete)]
		public async Task SoftDeleteRangeAsync(List<T> entities, int executingUserId)
		{
			using (var context = new TContext())
			{
				foreach (T entity in entities)
				{
					entity.is_deleted = true;
				}

				await UpdateRangeAsync(entities, executingUserId, [nameof(IMutationEntity.is_deleted)]);
			}
		}

		#endregion

		#region sync

		[EntityAuditAspect(OperationType = CrudOperationType.Delete)]
		public void HardDelete(T entity, int executingUserId)
		{
			using (var context = new TContext())
			{
				if (entity is null)
					throw new NotFoundException();

				var deletedEntity = context.Entry(entity);

				deletedEntity.State = EntityState.Deleted;

				context.SaveChanges();
			}
		}

		[EntityAuditAspect(OperationType = CrudOperationType.Delete)]
		public void SoftDelete(T entity, int executingUserId)
		{
			using (var context = new TContext())
			{
				if (entity is null)
					throw new NotFoundException();

				entity.is_deleted = true;

				Update(entity, executingUserId, [nameof(IMutationEntity.is_deleted)]);
			}
		}

		[EntityAuditAspect(OperationType = CrudOperationType.Delete)]
		public void SoftDeleteRange(List<T> entities, int executingUserId)
		{
			using (var context = new TContext())
			{
				foreach (T entity in entities)
				{
					entity.is_deleted = true;
				}

				UpdateRange(entities, executingUserId, [nameof(IMutationEntity.is_deleted)]);
			}
		}

		#endregion

		#endregion

	}

}