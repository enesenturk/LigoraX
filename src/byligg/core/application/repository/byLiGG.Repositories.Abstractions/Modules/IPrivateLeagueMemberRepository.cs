using Base.DataAccess.Repositories.Base.Abstract;
using byLiGG.Domain.Entities;

namespace byLiGG.Repositories.Abstractions.Modules
{
	public interface IPrivateLeagueMemberRepository : IAsyncMutationRepositoryBase<t_private_league_member>, IMutationRepositoryBase<t_private_league_member>
	{
	}
}
