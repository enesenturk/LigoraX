using Base.DataAccess.Repositories.Base.Abstract;
using byLiGG.Domain.Entities;

namespace byLiGG.Repositories.Abstractions.Modules
{
	public interface ITeamRepository : IAsyncMutationRepositoryBase<t_team>, IMutationRepositoryBase<t_team>
	{
	}
}
