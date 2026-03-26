using Base.DataAccess.Repositories.Base.Abstract;
using byLiGG.Domain.Entities;

namespace byLiGG.Repositories.Abstractions.Modules
{
	public interface IUserBadgeRepository : IAsyncMutationRepositoryBase<t_user_badge>, IMutationRepositoryBase<t_user_badge>
	{
	}
}
