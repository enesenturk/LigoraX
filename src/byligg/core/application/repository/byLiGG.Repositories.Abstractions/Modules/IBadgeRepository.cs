using Base.DataAccess.Repositories.Base.Abstract;
using byLiGG.Domain.Entities;

namespace byLiGG.Repositories.Abstractions.Modules
{
	public interface IBadgeRepository : IAsyncMutationRepositoryBase<t_badge>, IMutationRepositoryBase<t_badge>
	{
	}
}