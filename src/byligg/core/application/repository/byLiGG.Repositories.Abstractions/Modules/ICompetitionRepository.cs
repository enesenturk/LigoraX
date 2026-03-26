using Base.DataAccess.Repositories.Base.Abstract;
using byLiGG.Domain.Entities;

namespace byLiGG.Repositories.Abstractions.Modules
{
	public interface ICompetitionRepository : IAsyncMutationRepositoryBase<t_competition>, IMutationRepositoryBase<t_competition>
	{
	}
}
