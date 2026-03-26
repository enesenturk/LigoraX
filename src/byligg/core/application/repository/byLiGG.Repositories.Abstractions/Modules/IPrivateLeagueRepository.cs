using Base.DataAccess.Repositories.Base.Abstract;
using byLiGG.Domain.Entities;

namespace byLiGG.Repositories.Abstractions.Modules
{
	public interface IPrivateLeagueRepository : IAsyncMutationRepositoryBase<t_private_league>, IMutationRepositoryBase<t_private_league>
	{
	}
}
