using Base.Entity;

namespace Base.DataAccess.Repositories.Base.Abstract;

public interface IMutationRepositoryBase<T> : IReadRepositoryBase<T> where T : IMutationEntity
{

	#region Create

	T Add(T entity, Guid executingUserId);

	void AddRange(List<T> entities, Guid executingUserId);

	#endregion

	#region Update

	T Update(T entity, Guid executingUserId, params string[] fields);

	void UpdateRange(List<T> entities, Guid executingUserId, params string[] fields);

	#endregion

	#region Delete

	void HardDelete(T entity, Guid executingUserId);
	void SoftDelete(T entity, Guid executingUserId);
	void SoftDeleteRange(List<T> entities, Guid executingUserId);

	#endregion

}