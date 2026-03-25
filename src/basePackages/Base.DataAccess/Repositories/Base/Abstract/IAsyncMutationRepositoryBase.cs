using Base.Entity;

namespace Base.DataAccess.Repositories.Base.Abstract;

public interface IAsyncMutationRepositoryBase<T> : IAsyncReadRepositoryBase<T> where T : IMutationEntity
{

	#region Create

	Task<T> AddAsync(T entity, Guid executingUserId);

	Task AddRangeAsync(List<T> entities, Guid executingUserId);

	#endregion

	#region Update

	Task<T> UpdateAsync(T entity, Guid executingUserId, params string[] fields);

	Task UpdateRangeAsync(List<T> entities, Guid executingUserId, params string[] fields);

	#endregion

	#region Delete

	Task HardDeleteAsync(T entity, Guid executingUserId);
	Task SoftDeleteAsync(T entity, Guid executingUserId);
	Task SoftDeleteRangeAsync(List<T> entities, Guid executingUserId);

	#endregion

}