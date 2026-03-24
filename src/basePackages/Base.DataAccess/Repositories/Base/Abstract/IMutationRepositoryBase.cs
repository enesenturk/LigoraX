using Base.Entity;

namespace Base.DataAccess.Repositories.Base.Abstract;

public interface IMutationRepositoryBase<T> : IReadRepositoryBase<T> where T : IMutationEntity
{

	#region Create

	T Add(T entity, int executingUserId);

	void AddRange(List<T> entities, int executingUserId);

	#endregion

	#region Update

	T Update(T entity, int executingUserId, params string[] fields);

	void UpdateRange(List<T> entities, int executingUserId, params string[] fields);

	#endregion

	#region Delete

	void HardDelete(T entity, int executingUserId);
	void SoftDelete(T entity, int executingUserId);
	void SoftDeleteRange(List<T> entities, int executingUserId);

	#endregion

}