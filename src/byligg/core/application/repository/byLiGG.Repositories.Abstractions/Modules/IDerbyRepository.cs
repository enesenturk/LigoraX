using Base.DataAccess.Repositories.Base.Abstract;
using byLiGG.Domain.Entities;

namespace byLiGG.Repositories.Abstractions.Modules
{
	public interface IDerbyRepository : IAsyncMutationRepositoryBase<t_derby>, IMutationRepositoryBase<t_derby>
	{
	}
}
