using Base.DataAccess.Repositories.Base.Abstract;
using byLiGG.Domain.Entities;

namespace byLiGG.Repositories.Abstractions.Modules
{
	public interface IUserStatRepository : IAsyncMutationRepositoryBase<t_user_stat>, IMutationRepositoryBase<t_user_stat>
	{
	}
}