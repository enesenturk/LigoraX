using Base.DataAccess.Repositories.Base.Abstract;
using byLiGG.Domain.Entities;

namespace byLiGG.Repositories.Abstractions.Modules
{
	public interface ILeaderboardSnapshotRepository : IAsyncMutationRepositoryBase<t_leaderboard_snapshot>, IMutationRepositoryBase<t_leaderboard_snapshot>
	{
	}
}
