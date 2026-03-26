using Base.DataAccess.Repositories.Base.Abstract;
using byLiGG.Domain.Entities;

namespace byLiGG.Repositories.Abstractions.Modules
{
	public interface IScoringRuleRepository : IAsyncMutationRepositoryBase<t_scoring_rule>, IMutationRepositoryBase<t_scoring_rule>
	{
	}
}
