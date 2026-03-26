using Base.DataAccess.Repositories.Base.Concrete;
using byLiGG.Domain.Entities;
using byLiGG.Persistence.Contexts;
using byLiGG.Repositories.Abstractions.Modules;

namespace byLiGG.Repositories.Modules
{
	public class EfPredictionRepository : EfMutationRepositoryBase<t_prediction, byliggContext>, IPredictionRepository
	{
	}
}
