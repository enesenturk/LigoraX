using Base.Entity;

namespace Base.DataAccess.Repositories.Base.Abstract;

public interface IAsyncMutationRepositoryBase<T> : IAsyncReadRepositoryBase<T> where T : IMutationEntity
{

	#region Create

	Task<T> AddAsync(T entity, int executingUserId);

	Task AddRangeAsync(List<T> entities, int executingUserId);

	#endregion

	#region Update

	Task<T> UpdateAsync(T entity, int executingUserId, params string[] fields);

	Task UpdateRangeAsync(List<T> entities, int executingUserId, params string[] fields);

	#endregion

	#region Delete

	Task HardDeleteAsync(T entity, int executingUserId);
	Task SoftDeleteAsync(T entity, int executingUserId);
	Task SoftDeleteRangeAsync(List<T> entities, int executingUserId);

	#endregion

}