using Base.DataAccess.Repositories.Base.Abstract;
using byLiGG.Domain.Entities;

namespace byLiGG.Repositories.Abstractions.Modules
{
	public interface IMatchRepository : IAsyncMutationRepositoryBase<t_match>, IMutationRepositoryBase<t_match>
	{
	}
}