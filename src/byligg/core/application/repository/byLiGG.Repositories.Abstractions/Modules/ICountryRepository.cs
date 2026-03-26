using Base.DataAccess.Repositories.Base.Abstract;
using byLiGG.Domain.Entities;

namespace byLiGG.Repositories.Abstractions.Modules
{
	public interface ICountryRepository : IAsyncMutationRepositoryBase<t_country>, IMutationRepositoryBase<t_country>
	{
	}
}
