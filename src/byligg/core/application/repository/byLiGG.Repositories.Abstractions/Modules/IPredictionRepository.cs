using Base.DataAccess.Repositories.Base.Abstract;
using byLiGG.Domain.Entities;

namespace byLiGG.Repositories.Abstractions.Modules
{
	public interface IPredictionRepository : IAsyncMutationRepositoryBase<t_prediction>, IMutationRepositoryBase<t_prediction>
	{
	}
}